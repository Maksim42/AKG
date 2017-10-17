using System;
using System.Threading;
using System.Collections.Generic;
using SDL2;

namespace Lab3
{
    class MainWindow
    {
        private bool quit;
        private Thread mainThread;
        private IntPtr window;
        private int windowWidth, windowHeight;
        private WindowContext context;
        private Dictionary<string, Shape> shapesColection;
        /// <summary>
        /// Mouse selected shape
        /// </summary>
        Shape selectedShape;
        /// <summary>
        /// Shape move state
        /// </summary>
        private bool mousedrag = false;
        /// <summary>
        /// Animation controller
        /// </summary>
        private Animation animation;
        /// <summary>
        /// Animation state
        /// </summary>
        private bool animate = false;
        /// <summary>
        /// Mouse position after click
        /// </summary>
        private Point mousePoint;

        public MainWindow(int width, int height)
        {
            quit = false;

            windowWidth = width;
            windowHeight = height;
           
            mainThread = new Thread(MainCycle);
        }

        public void Shown()
        {
            mainThread.Start();
            mainThread.Join();
        }

        public void Close()
        {
            quit = true;
            mainThread.Join();
        }

        private void MainCycle()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            window = SDL.SDL_CreateWindow("AKG Lab3",
                                          100, 100,
                                          windowWidth, windowHeight,
                                          SDL.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            context = new WindowContext(window, windowWidth, windowHeight);

            InitShapes();

            while (!quit)
            {
                SDL.SDL_Event sdlEvent;
                int containEvent = SDL.SDL_PollEvent(out sdlEvent);

                if (containEvent == 1)  // Check event pool return event
                {
                    switch (sdlEvent.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT:
                            {
                                quit = true;
                                break;
                            }
                        case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                            {
                                MouseButtonDownHandler(sdlEvent);
                                break;
                            }
                        case SDL.SDL_EventType.SDL_MOUSEMOTION:
                            {
                                MouseMoveHandler(sdlEvent);
                                break;
                            }
                        case SDL.SDL_EventType.SDL_KEYDOWN:
                            {
                                var key = sdlEvent.key;
                                switch (key.keysym.sym)
                                {
                                    case SDL.SDL_Keycode.SDLK_a:
                                        {
                                            A_KeyDownHandler();
                                            break;
                                        }
                                    //case SDL.SDL_Keycode.SDLK_UP:
                                    //    {
                                    //        ChangeParamA(1);
                                    //        break;
                                    //    }

                                }
                                break;
                            }
                    }
                }
                Draw();
                Thread.Sleep(10);
            }

            context.Close();
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }

    #region Handlers
        private void MouseButtonDownHandler(SDL.SDL_Event e)
        {
            var mouseDownEvent = e.button;
            mousePoint.x = context.XrT(mouseDownEvent.x);
            mousePoint.y = context.YrT(mouseDownEvent.y);

            if (!mousedrag)
            {
                selectedShape = SelectShape(mousePoint);

                if (selectedShape != null)
                {
                    mousedrag = true;
                    animate = false;
                    animation.Reset();
                    selectedShape.Move(mousePoint);
                }
            }
            else
            {
                mousedrag = false;
            }            
        }

        private void MouseMoveHandler(SDL.SDL_Event e)
        {
            var moveEvent = e.motion;

            if (mousedrag)
            {
                mousePoint.x = context.XrT(moveEvent.x);
                mousePoint.y = context.YrT(moveEvent.y);
                selectedShape.Move(mousePoint);
            }
        }

        private void A_KeyDownHandler()
        {
            if (!mousedrag)
            {
                animate = !animate;
            }
        }
    #endregion Handlers

        /// <summary>
        /// Select shape by context position
        /// </summary>
        /// <remarks>If no shape return null</remarks>
        /// <param name="x">x context position</param>
        /// <param name="y">y context position</param>
        /// <returns>Selected shape or null if no shape in position</returns>
        private Shape SelectShape(Point selectPosition)
        {
            foreach (var shapeRecord in shapesColection)
            {
                bool result = shapeRecord.Value.PointIn(mousePoint);

                if (result)
                {
                    return shapeRecord.Value;
                }
            }

            return null;
        }

        private void InitShapes()
        {
            shapesColection = new Dictionary<string, Shape>();
            shapesColection["Ellipse"] = new Shapes.Ellipse(context, 100, 50, 400, 400);
            shapesColection["Trapeze"] = new Shapes.Trapeze(context, 100, 50, 0.4, 100, 100);
            shapesColection["CutWindow"] = new Shapes.CutWindow(context, 150, 100, 200, 200);

            shapesColection["Ellipse"].CrossingShape = shapesColection["CutWindow"];
            shapesColection["Trapeze"].CrossingShape = shapesColection["Ellipse"];

            animation = new Animation(shapesColection["Ellipse"], shapesColection["Trapeze"]);

            // Mouse click point
            mousePoint = new Point(0, 0);
        }

        private void Draw()
        {
            context.UpdateParameters();
            context.Clear();

            SDL.SDL_SetRenderDrawColor(context.Render, 0, 0, 0, 0);

            // Animation step
            if (animate)
            {
                animation.Play();
            }
            
            ShapesDraw();

            // Draw WindowContext border
            context.DrawLine(0, 0, 0, context.Height);
            context.DrawLine(0, context.Height, context.Width, context.Height);
            context.DrawLine(context.Width, context.Height, context.Width, 0);
            context.DrawLine(context.Width, 0, 0, 0);

            context.RefreshWindow();
        }

        private void ShapesDraw()
        {
            foreach (var shapeRecord in shapesColection)
            {
                shapeRecord.Value.Draw();
            }
        }
    }
}
