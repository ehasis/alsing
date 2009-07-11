namespace CompositeDiagrammer
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    using QI4N.Framework;
    using QI4N.Framework.Bootstrap;
    using QI4N.Framework.Runtime;

    public partial class Form1 : Form
    {
        private readonly IList<Shape> elements = new List<Shape>();

        public Form1()
        {
            this.InitializeComponent();
        }

        private static LayerAssembly CreateDomainLayer(ApplicationAssembly app)
        {
            LayerAssembly layer = app.NewLayerAssembly("DomainLayer");

            ModuleAssembly shapeModule = CreateShapeModule(layer);

            return layer;
        }

        private static ModuleAssembly CreateShapeModule(LayerAssembly layer)
        {
            ModuleAssembly module = layer.NewModuleAssembly("ShapeModule");

            module.AddTransients()
                    .Include<DrawingService>();
            module
                    .AddTransients()
                    .Include<RectangleShape>()
                    .Include<EllipseShape>()
                    .Include<LineShape>()
                    //.Include<DescriptionTransient>()
                    .Include<GroupShape>()
                    ;

            return module;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var f = new ApplicationAssemblyFactory();

            ApplicationAssembly app = f.NewApplicationAssembly();

            LayerAssembly domainLayer = CreateDomainLayer(app);

            ApplicationModel applicationModel = ApplicationModel.NewModel(app);

            ApplicationInstance applicationInstance = applicationModel.NewInstance();

            Module shapeModule = applicationInstance.FindModule("DomainLayer", "ShapeModule");

            var drawing = shapeModule.TransientBuilderFactory.NewTransient<Drawing>();

            var rectangle = drawing.Create<RectangleShape>();
            drawing.Remove(rectangle);
            rectangle.SetBounds(100, 100, 200, 200);
            rectangle.Rotate(1.5);

            var ellipse = drawing.Create<EllipseShape>();
            ellipse.SetBounds(300, 100, 200, 300);

            var line = drawing.Create<LineShape>();
            line.MoveNode(0, 50, 150);
            line.MoveNode(1, 500, 300);

            GroupShape group = drawing.Group(ellipse, rectangle, line);

            this.elements.Add(group);
        }

        private void viewPort1_Paint(object sender, PaintEventArgs e)
        {
            var renderInfo = new RenderInfo
                                 {
                                         Graphics = e.Graphics
                                 };

            foreach (Shape element in this.elements)
            {
                element.Render(renderInfo);
            }
        }
    }
}