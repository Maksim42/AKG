using System;
using System.Threading;
using System.Collections.Generic;
using SDL2;
using SDLGeometry;
using Shape3;

namespace SDLWindow
{
    class MainWindow
    {
        private bool quit;
        private Thread mainThread;
        private IntPtr window;
        private int windowWidth, windowHeight;
        private WindowContext context;
        private Shape3C donut, cube;
        private Shape3C shape;
        
        public MainWindow(int width, int height)
        {
            quit = false;

            windowWidth = width;
            windowHeight = height;
           
            mainThread = new Thread(MainCycle);
            mainThread.Start();
        }

        public void Close()
        {
            quit = true;
            mainThread.Join();
        }

        private void MainCycle()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            window = SDL.SDL_CreateWindow("AKG Lab4",
                                          100, 100,
                                          windowWidth, windowHeight,
                                          SDL.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            context = new WindowContext(window, windowWidth, windowHeight);

            Line.context = context;
            Shape3C.context = context;
            Surface.SetContext(context);

            InitShapes();

            while (!quit)
            {
                int containEvent = SDL.SDL_PollEvent(out SDL.SDL_Event sdlEvent);

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
                        case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                            {
                                MouseButtonUpHandler(sdlEvent);
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
                                    case SDL.SDL_Keycode.SDLK_RETURN:
                                        {
                                            Enter_KeyDownHandler(sdlEvent);
                                            break;
                                        }
                                    case SDL.SDL_Keycode.SDLK_LCTRL:
                                        {
                                            LCtrl_KeyDownHandler();
                                            break;
                                        }
                                    case SDL.SDL_Keycode.SDLK_LALT:
                                        {
                                            LAlt_KeyDownHandler();
                                            break;
                                        }
                                    case SDL.SDL_Keycode.SDLK_LSHIFT:
                                        {
                                            LShift_KeyDownHandler();
                                            break;
                                        }
                                    case SDL.SDL_Keycode.SDLK_EQUALS:
                                        {
                                            Equals_KeyDownHandler();
                                            break;
                                        }
                                    case SDL.SDL_Keycode.SDLK_1:
                                        {
                                            Shape1_KeyDownHandler();
                                            break;
                                        }
                                    case SDL.SDL_Keycode.SDLK_2:
                                        {
                                            Shape2_KeyDownHandler();
                                            break;
                                        }
                                    case SDL.SDL_Keycode.SDLK_TAB:
                                        {
                                            TAB_KeyDownHandler();
                                            break;
                                        }
                                }
                                break;
                            }

                        case SDL.SDL_EventType.SDL_KEYUP:
                            {
                                var key = sdlEvent.key;
                                switch (key.keysym.sym)
                                {
                                    case SDL.SDL_Keycode.SDLK_LCTRL:
                                        {
                                            LCtrl_KeyUpHandler();
                                            break;
                                        }
                                    case SDL.SDL_Keycode.SDLK_LALT:
                                        {
                                            LAlt_KeyUpHandler();
                                            break;
                                        }
                                    case SDL.SDL_Keycode.SDLK_LSHIFT:
                                        {
                                            LShift_KeyUpHandler();
                                            break;
                                        }
                                }
                                break;
                            }
                        case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                            {
                                if (sdlEvent.wheel.y == 1)
                                {
                                    MouseWhellUpHandler();
                                } else if (sdlEvent.wheel.y == -1)
                                {
                                    MouseWhellDownHandler();
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

            // Close application
            Environment.Exit(0);
        }

        #region Handlers
        int startX, startY;
        bool mouseDrag = false;
        bool animation = false;
        bool zRotation = false;
        bool tRotation = false;
        bool perspRotation = false;

        private void MouseButtonDownHandler(SDL.SDL_Event e)
        {
            var mouseDownEvent = e.button;

            // check left button click
            if (mouseDownEvent.button != SDL.SDL_BUTTON_LEFT)
            {
                return;
            }

            startX = mouseDownEvent.x;
            startY = mouseDownEvent.y;

            mouseDrag = true;
            animation = false;
        }

        private void MouseButtonUpHandler(SDL.SDL_Event e)
        {
            var mouseDownEvent = e.button;

            // check left button click
            if (mouseDownEvent.button != SDL.SDL_BUTTON_LEFT)
            {
                return;
            }

            mouseDrag = false;
        }

        private void MouseMoveHandler(SDL.SDL_Event e)
        {
            var moveEvent = e.motion;
            SDL.SDL_GetWindowSize(window, out int fullWidth, out int fullHeight);

            if (mouseDrag)
            {
                if (!zRotation && !tRotation && !perspRotation)
                {
                    shape.yAngle += rt(startX - moveEvent.x, fullWidth);
                    shape.xAngle += rt(startY - moveEvent.y, fullHeight);
                }
                else
                {
                    if (perspRotation)
                    {
                        shape.phiAngle += rt(startY - moveEvent.y, fullHeight);
                        shape.tethaAngle += rt(startX - moveEvent.x, fullWidth);
                    }
                    if (zRotation)
                    {
                        shape.zAngle += rt(startY - moveEvent.y, fullHeight);
                    }
                    if (tRotation)
                    {
                        shape.tAngle += rt(startX - moveEvent.x, fullWidth);
                    }
                }

                startX = moveEvent.x;
                startY = moveEvent.y;
            }
        }
        private Func<double, double, double> rt = (l, f) => (l / f * Math.PI * 2);

        private void Enter_KeyDownHandler(SDL.SDL_Event e)
        {
            var keyEvent = e.key;

            if (keyEvent.repeat != 0)
            {
                return;
            }

            animation = !animation;
        }

        private void Equals_KeyDownHandler()
        {
            shape.xAngle = 0;
            shape.yAngle = 0;
            shape.zAngle = 0;
            shape.tAngle = 0;
            shape.X = 0;
            shape.Y = 0;
            shape.Z = 0;
            shape.Distance = 60;
            shape.ScreenDistance = 30;
            shape.phiAngle = 0;
            shape.tethaAngle = 0;
            shape.Scale = 1;
        }

        private void Shape1_KeyDownHandler()
        {
            shape = donut;
        }

        private void Shape2_KeyDownHandler()
        {
            shape = cube;
        }

        private void TAB_KeyDownHandler()
        {
            //shape.perspective = !shape.perspective;
        }

        // LCtrl key handler
        private void LCtrl_KeyDownHandler() => zRotation = true;
        private void LCtrl_KeyUpHandler() => zRotation = false;
        
        // LAlt key handler
        private void LAlt_KeyDownHandler() => tRotation = true;
        private void LAlt_KeyUpHandler() => tRotation = false;

        // LShift key handler
        private void LShift_KeyDownHandler() => perspRotation = true;
        private void LShift_KeyUpHandler() => perspRotation = false;

        private void MouseWhellUpHandler()
        {
            if (!zRotation)
            {
                shape.Distance -= 1;
            }
            else
            {
                shape.ScreenDistance += 1;
            }
        }

        private void MouseWhellDownHandler()
        {
            if (!zRotation)
            {
                shape.Distance += 1;
            }
            else
            {
                shape.ScreenDistance -= 1;
            }
        }
        #endregion Handlers

        private void Draw()
        {
            context.UpdateParameters();
            context.Clear();

            //SDL.SDL_SetRenderDrawColor(context.Render, 0, 0, 0, 0);
            context.SetColor(SDLColor.Color.Black);

            // Draw axis
            context.DrawLine(0, 0, 0, context.Height);
            context.DrawLine(context.Width, 0, 0, 0);

            context.Zbufer.Unvalidate();
            shape.Unvalidate();
            shape.Draw();

            if (animation)
            {
                shape.xAngle += 0.02;
                shape.yAngle += 0.01;
                shape.zAngle += 0.007;
                shape.tAngle += 0.013;
                //shape.Y += 0.1;
                //shape.Z += 0.1;
                //shape.Scale -= 0.01;
            }

            context.RefreshWindow();
        }

        private void InitShapes()
        {
            cube = new Cube(60);
            cube.perspective = true;
            donut = new SquareDonat(50);

            shape = donut;
        }
    }
}
