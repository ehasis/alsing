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
        private readonly IList<ElementComposite> elements = new List<ElementComposite>();

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

            module
                    .AddTransients()
                    .Include<RectangleShape>()
                    .Include<EllipseShape>()
                    //.Include<DescriptionTransient>()
                    //.Include<GroupTransient>()
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

            var rectangle = shapeModule.TransientBuilderFactory.NewTransient<RectangleShape>();
            rectangle.SetBounds(100, 100, 200, 200);

            var ellipse = shapeModule.TransientBuilderFactory.NewTransient<EllipseShape>();
            ellipse.SetBounds(300, 200, 200, 400);

            this.elements.Add(rectangle);
            this.elements.Add(ellipse);
        }

        private void viewPort1_Paint(object sender, PaintEventArgs e)
        {
            var renderInfo = new RenderInfo();
            renderInfo.Graphics = e.Graphics;

            foreach (ElementComposite element in this.elements)
            {
                element.Render(renderInfo);
            }
        }
    }
}