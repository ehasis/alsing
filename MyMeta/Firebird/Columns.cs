using System;
using System.Data;
using System.Runtime.InteropServices;
using FirebirdSql.Data.FirebirdClient;

namespace MyMeta.Firebird
{
    public class FirebirdColumns : Columns
    {
        internal DataColumn f_AutoKey;
        internal DataColumn f_TypeName;
        internal DataColumn f_TypeNameComplete;

        internal override void LoadForTable()
        {
            try
            {
                var cn = new FbConnection(_dbRoot.ConnectionString);
                cn.Open();
                DataTable metaData = cn.GetSchema("Columns", new[] {null, null, Table.Name});

                PopulateArray(metaData);
                LoadExtraData(cn, Table.Name, "T");
                cn.Close();
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }

        internal override void LoadForView()
        {
            try
            {
                var cn = new FbConnection(_dbRoot.ConnectionString);
                cn.Open();

                DataTable metaData = cn.GetSchema("Columns", new[] {null, null, View.Name});

                PopulateArray(metaData);
                LoadExtraData(cn, View.Name, "V");
                cn.Close();
            }
            catch (Exception ex)
            {
                string m = ex.Message;
            }
        }

        private void LoadExtraData(FbConnection cn, string name, string type)
        {
            try
            {
                int dialect = 1;
                object o = null;
                short scale = 0;

                try
                {
                    var cnString = new FbConnectionStringBuilder(cn.ConnectionString);
                    dialect = cnString.Dialect;
                }
                catch {}

                string select =
                    "select r.rdb$field_name, f.rdb$field_scale AS SCALE, f.rdb$computed_source AS IsComputed, f.rdb$field_type as FTYPE, f.rdb$field_sub_type AS SUBTYPE, f.rdb$dimensions AS DIM from rdb$relation_fields r, rdb$types t, rdb$fields f where r.rdb$relation_name='" +
                    name +
                    "' and f.rdb$field_name=r.rdb$field_source and t.rdb$field_name='RDB$FIELD_TYPE' and f.rdb$field_type=t.rdb$type order by r.rdb$field_position;";

                // Column Data
                var adapter = new FbDataAdapter(select, cn);
                var dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Dimension Data
                string dimSelect =
                    "select r.rdb$field_name AS Name , d.rdb$dimension as DIM, d.rdb$lower_bound as L, d.rdb$upper_bound as U from rdb$fields f, rdb$field_dimensions d, rdb$relation_fields r where r.rdb$relation_name='" +
                    name +
                    "' and f.rdb$field_name = d.rdb$field_name and f.rdb$field_name=r.rdb$field_source order by d.rdb$dimension;";

                var dimAdapter = new FbDataAdapter(dimSelect, cn);
                var dimTable = new DataTable();
                dimAdapter.Fill(dimTable);

                if (_array.Count > 0)
                {
                    var col = _array[0] as Column;

                    f_TypeName = new DataColumn("TYPE_NAME", typeof (string));
                    col._row.Table.Columns.Add(f_TypeName);

                    f_TypeNameComplete = new DataColumn("TYPE_NAME_COMPLETE", typeof (string));
                    col._row.Table.Columns.Add(f_TypeNameComplete);

                    f_IsComputed = new DataColumn("IS_COMPUTED", typeof (bool));
                    col._row.Table.Columns.Add(f_IsComputed);

                    short ftype = 0;
                    short dim = 0;
                    DataRowCollection rows = dataTable.Rows;

                    int count = _array.Count;
                    Column c = null;

                    for (int index = 0; index < count; index++)
                    {
                        c = (Column) this[index];

                        if (!c._row.IsNull("DOMAIN_NAME"))
                        {
                            // Special Hack, if there is a domain 
                            c._row["TYPE_NAME"] = c._row["DOMAIN_NAME"] as string;
                            c._row["TYPE_NAME_COMPLETE"] = c._row["DOMAIN_NAME"] as string;

                            o = rows[index]["IsComputed"];
                            if (o != DBNull.Value)
                            {
                                c._row["IS_COMPUTED"] = true;
                            }

                            scale = (short) rows[index]["SCALE"];
                            if (scale < 0)
                            {
                                c._row["NUMERIC_SCALE"] = Math.Abs(scale);
                            }
                            continue;
                        }

                        try
                        {
                            var bigint = c._row["COLUMN_DATA_TYPE"] as string;
                            if (bigint.ToUpper() == "BIGINT")
                            {
                                c._row["TYPE_NAME"] = "BIGINT";
                                c._row["TYPE_NAME_COMPLETE"] = "BIGINT";
                                continue;
                            }
                        }
                        catch {}

//		public const int blr_text		= 14;
//		public const int blr_text2		= 15;
//		public const int blr_short		= 7;
//		public const int blr_long		= 8;
//		public const int blr_quad		= 9;
//		public const int blr_int64		= 16;
//		public const int blr_float		= 10;
//		public const int blr_double		= 27;
//		public const int blr_d_float	= 11;
//		public const int blr_timestamp	= 35;
//		public const int blr_varying	= 37;
//		public const int blr_varying2	= 38;
//		public const int blr_blob		= 261;
//		public const int blr_cstring	= 40;
//		public const int blr_cstring2	= 41;
//		public const int blr_blob_id	= 45;
//		public const int blr_sql_date	= 12;
//		public const int blr_sql_time	= 13;

                        // Step 1: DataTypeName
                        ftype = (short) rows[index]["FTYPE"];

                        switch (ftype)
                        {
                            case 7:
                                c._row["TYPE_NAME"] = "SMALLINT";
                                break;

                            case 8:
                                c._row["TYPE_NAME"] = "INTEGER";
                                break;

                            case 9:
                                c._row["TYPE_NAME"] = "QUAD";
                                break;

                            case 10:
                                c._row["TYPE_NAME"] = "FLOAT";
                                break;

                            case 11:
                                c._row["TYPE_NAME"] = "DOUBLE PRECISION";
                                break;

                            case 12:
                                c._row["TYPE_NAME"] = "DATE";
                                break;

                            case 13:
                                c._row["TYPE_NAME"] = "TIME";
                                break;

                            case 14:
                                c._row["TYPE_NAME"] = "CHAR";
                                break;

                            case 16:
                                c._row["TYPE_NAME"] = "NUMERIC";
                                break;

                            case 27:
                                c._row["TYPE_NAME"] = "DOUBLE PRECISION";
                                break;

                            case 35:

                                if (dialect > 2)
                                {
                                    c._row["TYPE_NAME"] = "TIMESTAMP";
                                }
                                else
                                {
                                    c._row["TYPE_NAME"] = "DATE";
                                }
                                break;

                            case 37:
                                c._row["TYPE_NAME"] = "VARCHAR";
                                break;

                            case 40:
                                c._row["TYPE_NAME"] = "CSTRING";
                                break;

                            case 261:
                                var subtype = (short) rows[index]["SUBTYPE"];

                                switch (subtype)
                                {
                                    case 0:
                                        c._row["TYPE_NAME"] = "BLOB(BINARY)";
                                        break;
                                    case 1:
                                        c._row["TYPE_NAME"] = "BLOB(TEXT)";
                                        break;
                                    default:
                                        c._row["TYPE_NAME"] = "BLOB(UNKNOWN)";
                                        break;
                                }
                                break;
                        }

                        scale = (short) rows[index]["SCALE"];
                        if (scale < 0)
                        {
                            c._row["TYPE_NAME"] = "NUMERIC";
                            c._row["NUMERIC_SCALE"] = Math.Abs(scale);
                        }

                        o = rows[index]["IsComputed"];
                        if (o != DBNull.Value)
                        {
                            c._row["IS_COMPUTED"] = true;
                        }

                        // Step 2: DataTypeNameComplete
                        var s = c._row["TYPE_NAME"] as string;
                        switch (s)
                        {
                            case "VARCHAR":
                            case "CHAR":
                                c._row["TYPE_NAME_COMPLETE"] = s + "(" + c.CharacterMaxLength + ")";
                                break;

                            case "NUMERIC":

                                switch ((int) c._row["COLUMN_SIZE"])
                                {
                                    case 2:
                                        c._row["TYPE_NAME_COMPLETE"] = s + "(4, " + c.NumericScale + ")";
                                        break;
                                    case 4:
                                        c._row["TYPE_NAME_COMPLETE"] = s + "(9, " + c.NumericScale + ")";
                                        break;
                                    case 8:
                                        c._row["TYPE_NAME_COMPLETE"] = s + "(15, " + c.NumericScale + ")";
                                        break;
                                    default:
                                        c._row["TYPE_NAME_COMPLETE"] = "NUMERIC(18,0)";
                                        break;
                                }
                                break;

                            case "BLOB(TEXT)":
                            case "BLOB(BINARY)":
                                c._row["TYPE_NAME_COMPLETE"] = "BLOB";
                                break;

                            default:
                                c._row["TYPE_NAME_COMPLETE"] = s;
                                break;
                        }

                        s = c._row["TYPE_NAME_COMPLETE"] as string;

                        dim = 0;
                        o = rows[index]["DIM"];
                        if (o != DBNull.Value)
                        {
                            dim = (short) o;
                        }

                        if (dim > 0)
                        {
                            dimTable.DefaultView.RowFilter = "Name = '" + c.Name + "'";
                            dimTable.DefaultView.Sort = "DIM";

                            string a = "[";
                            bool bFirst = true;

                            foreach (DataRowView vrow in dimTable.DefaultView)
                            {
                                DataRow row = vrow.Row;

                                if (!bFirst) a += ",";

                                a += row["L"] + ":" + row["U"];

                                bFirst = false;
                            }

                            a += "]";

                            c._row["TYPE_NAME_COMPLETE"] = s + a;

                            c._row["TYPE_NAME"] = c._row["TYPE_NAME"] + ":A";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string e = ex.Message;
            }
        }


        //function ibfieldtypetosource(value:integer;size:integer;scale:integer;dialect:integer):string;
        //begin
        // case value of
        //  7:
        //   Result:='SMALLINT';
        //  8:
        //   Result:='INTEGER';
        //  9:
        //   Result:='QUAD';
        //  10:
        //   Result:='FLOAT';
        //  11:
        //   Result:='DOUBLE PRECISION';
        //  12:
        //   Result:='DATE';
        //  13:
        //   Result:='TIME';
        //  14:
        //   Result:='CHAR('+inttostr(size)+')';
        //  16:
        //   // INT64
        //   Result:='NUMERIC';
        //  27:
        //   Result:='DOUBLE PRECISION';
        //  35:
        //   if Dialect>2 then
        //    Result:='TIMESTAMP'
        //   else
        //    Result:='DATE';
        //  37:
        //   Result:='VARCHAR('+inttostr(size)+')';
        //  40:
        //   Result:='CSTRING('+inttostr(size)+')';
        //  261:
        //   begin
        //    Result:='BLOB';
        //   end;
        //  else
        //   Result:='';
        // end;
        // if scale<0 then
        // begin
        //  Result:='NUMERIC';
        //  case size of
        //   2:
        //    REsult:=Result+'('+Inttostr(4)+','+inttostr(-scale)+')';
        //   4:
        //    REsult:=Result+'('+Inttostr(9)+','+inttostr(-scale)+')';
        //   8:
        //    begin
        //     if dialect=1 then
        //      REsult:=Result+'('+Inttostr(15)+','+inttostr(-scale)+')'
        //     else
        //      REsult:=Result+'('+Inttostr(18)+','+inttostr(-scale)+')'
        //    end;
        //   else
        //     REsult:=Result+'('+Inttostr(15)+','+inttostr(-scale)+')';
        //  end;
        // end
        // else
        // begin
        //  if Result='NUMERIC' then
        //   Result:='NUMERIC(18,0)';
        // end;
        //end;
    }
}

// Fills the IBAccess Fields Grid on the table definition

//procedure Filldatasetwithdomainsinfo(base:TIBDatabase;viewsystem:boolean;data:TDataset;fprogres:TFFetching);
//var
// Tran1:TIBTransaction;
// QDomain:TIBSQL;
// QSearch:TIBSQL;
// dinfo:TIBDefDomain;
//begin
// Tran1:=TIBTransaction.create(Application);
// try
//  tran1.DefaultDatabase:=base;
//  QDomain:=TIBSQL.Create(Application);
//  QSearch:=TIBSQL.Create(Application);
//  try
//   QSearch.database:=base;
//   QSearch.transaction:=tran1;
//   QDomain.database:=base;
//   QDomain.transaction:=tran1;
//   tran1.StartTransaction;
//   try
//    QDomain.SQL.TExt:='SELECT RDB$FIELD_NAME DOMINI,RDB$VALIDATION_SOURCE VALIDA,RDB$COMPUTED_SOURCE CALCULA,'+
//     'RDB$DEFAULT_SOURCE OMISION,RDB$FIELD_LENGTH TAMANY,RDB$SEGMENT_LENGTH SEGLEN,'+
//     'RDB$FIELD_SCALE ESCALA,RDB$NULL_FLAG NONULO,'+
//     'RDB$CHARACTER_SET_ID CHARSET,RDB$COLLATION_ID ORDRE,'+
//      'RDB$FIELD_TYPE TIPO,RDB$FIELD_SUB_TYPE SUBTIPO,'+
//     'RDB$DIMENSIONS DIMENSIO,RDB$CHARACTER_LENGTH CHARLEN'+
//     ' FROM RDB$FIELDS ';
//    if Not viewsystem then
//     QDomain.SQL.TExt:=QDomain.SQL.TExt+' WHERE ( (RDB$SYSTEM_FLAG=0) or (RDB$SYSTEM_FLAG IS NULL) ) ';
//    QDomain.SQL.TExt:=QDomain.SQL.TExt+'ORDER BY 1';
//    QDomain.ExecQuery;
//    try
//     While Not QDomain.EOF do
//     begin
//      dinfo:=FillDomainFromQuerys(QDomain,QSearch,base.sqldialect,fprogres);
//      data.Append;
//      try
//       data.FieldByName('FIELDNAME').Value:=dinfo.name;
//       data.FieldByName('DESCRIP').Value:=ibfieldtypetosource(dinfo.basetype,dinfo.size,dinfo.scale,base.sqldialect)
//        +dinfo.arraysource;
//       if dinfo.Notnull then
//        data.FieldByName('NULLS').Value:=SRpNo
//       else
//        data.FieldByName('NULLS').Value:=SRpYes;
//       data.FieldByName('CHECK').Value:=dinfo.check;
//       data.FieldByName('DEFAULT').Value:=dinfo.default;
//       data.FieldByName('COMPUTED').Value:=dinfo.computed;
//       data.FieldByName('CHARACTER_SET').Value:=dinfo.character_set;
//       data.FieldByName('COLLATION').Value:=dinfo.collation;
//       data.FieldByName('SUBTYPE').Value:=dinfo.subtype;
//       data.FieldByName('SEGSIZE').Value:=dinfo.segmentsize;
//       data.FieldByName('SIZEBYTES').Value:=dinfo.sizebytes;
//       data.post;
//      except
//       data.cancel;
//       raise;
//      end;
//      QDomain.Next;
//      if assigned(fprogres)then
//      begin
//       fprogres.IncFetch(1);
//      end;
//     end;
//    finally
//     QDomain.CLose;
//    end;
//    tran1.commitretaining;
//   except
//    tran1.rollback;
//    raise;
//   end;
//  finally
//   QSearch.free;
//   QDomain.free;
//  end;
// finally
//  tran1.free;
// end;
//end;
//
//function ibfieldtypetosource(value:integer;size:integer;scale:integer;dialect:integer):string;
//begin
// case value of
//  7:
//   Result:='SMALLINT';
//  8:
//   Result:='INTEGER';
//  9:
//   Result:='QUAD';
//  10:
//   Result:='FLOAT';
//  11:
//   Result:='DOUBLE PRECISION';
//  12:
//   Result:='DATE';
//  13:
//   Result:='TIME';
//  14:
//   Result:='CHAR('+inttostr(size)+')';
//  16:
//   // INT64
//   Result:='NUMERIC';
//  27:
//   Result:='DOUBLE PRECISION';
//  35:
//   if Dialect>2 then
//    Result:='TIMESTAMP'
//   else
//    Result:='DATE';
//  37:
//   Result:='VARCHAR('+inttostr(size)+')';
//  40:
//   Result:='CSTRING('+inttostr(size)+')';
//  261:
//   begin
//    Result:='BLOB';
//   end;
//  else
//   Result:='';
// end;
// if scale<0 then
// begin
//  Result:='NUMERIC';
//  case size of
//   2:
//    REsult:=Result+'('+Inttostr(4)+','+inttostr(-scale)+')';
//   4:
//    REsult:=Result+'('+Inttostr(9)+','+inttostr(-scale)+')';
//   8:
//    begin
//     if dialect=1 then
//      REsult:=Result+'('+Inttostr(15)+','+inttostr(-scale)+')'
//     else
//      REsult:=Result+'('+Inttostr(18)+','+inttostr(-scale)+')'
//    end;
//   else
//     REsult:=Result+'('+Inttostr(15)+','+inttostr(-scale)+')';
//  end;
// end
// else
// begin
//  if Result='NUMERIC' then
//   Result:='NUMERIC(18,0)';
// end;
//end;
//
//function ibfieldtypetostringtype(value:integer;scale:integer;dialect:integer):string;
//begin
// case value of
//  7:
//   Result:='SMALLINT';
//  8:
//   Result:='INTEGER';
//  9:
//   Result:='QUAD';
//  10:
//   Result:='FLOAT';
//  11:
//   Result:='DOUBLE PRECISION';
//  12:
//   Result:='DATE';
//  13:
//   Result:='TIME';
//  14:
//   Result:='CHAR';
//  16:
//   // INT64
//   Result:='NUMERIC';
//  27:
//   Result:='DOUBLE PRECISION';
//  35:
//   if Dialect>2 then
//    Result:='TIMESTAMP'
//   else
//    Result:='DATE';
//  37:
//   Result:='VARCHAR';
//  40:
//   Result:='CSTRING';
//  261:
//   begin
//    Result:='BLOB';
//   end;
//  else
//   Result:='';
// end;
// if scale<0 then
//  result:='NUMERIC';
//end;
//
//function ibfieldtypetosourcescale(value:integer;size:integer;scale:integer;var precision,precscale:variant;dialect:integer):string;
//begin
// precision:=null;
// precscale:=null;
// case value of
//  7:
//   Result:='SMALLINT';
//  8:
//   Result:='INTEGER';
//  9:
//   Result:='QUAD';
//  10:
//   Result:='FLOAT';
//  11:
//   Result:='DOUBLE PRECISION';
//  12:
//   Result:='DATE';
//  13:
//   Result:='TIME';
//  14:
//   begin
//    Result:='CHAR';
//   end;
//  16:
//   // INT64
//   Result:='NUMERIC';
//  27:
//   Result:='DOUBLE PRECISION';
//  35:
//   if Dialect>2 then
//    Result:='TIMESTAMP'
//   else
//    Result:='DATE';
//  37:
//   begin
//    Result:='VARCHAR';
//   end;
//  40:
//   begin
//    Result:='CSTRING';
//   end;
//  261:
//   begin
//    Result:='BLOB';
//   end;
//  else
//   Result:='';
// end;
// if scale<0 then
// begin
//  Result:='NUMERIC';
//  case size of
//   2:
//    begin
//     precision:=4;
//     precscale:=-scale;
//    end;
//   4:
//    begin
//     precision:=9;
//     precscale:=-scale;
//    end;
//   8:
//    begin
//     if dialect=1 then
//      precision:=15
//     else
//      precision:=18;
//     precscale:=-scale;
//    end;
//   else
//    begin
//     precision:=15;
//     precscale:=-scale;
//    end;
//  end;
// end
// else
// begin
//  if Result='NUMERIC' then
//   Result:='NUMERIC(18,0)';
// end;
//end;