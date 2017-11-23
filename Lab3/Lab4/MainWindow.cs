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
        private Shape3C shape;
        
        public MainWindow(int width, int height)
        {
            quit = false;

            windowWidth = width;
            windowHeight = height;
           
            mainThread = new Thread(MainCycle);
            mainThread.Start();
        }

        // ???
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
            window = SDL.SDL_CreateWindow("AKG Lab4",
                                          100, 100,
                                          windowWidth, windowHeight,
                                          SDL.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            context = new WindowContext(window, windowWidth, windowHeight);

            Line.context = context;

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
                                        //case SDL.SDL_Keycode.SDLK_a:
                                        //    {
                                        //        A_KeyDownHandler();
                                        //        break;
                                        //    }
                                        //case SDL.SDL_Keycode.SDLK_UP:
                                        //    {
                                        //        UP_KeyDownHandler();
                                        //        break;
                                        //    }
                                        //case SDL.SDL_Keycode.SDLK_DOWN:
                                        //    {
                                        //        DOWN_KeyDownHandler();
                                        //        break;
                                        //    }
                                        //case SDL.SDL_Keycode.SDLK_RIGHT:
                                        //    {
                                        //        RIGHT_KeyDownHandler();
                                        //        break;
                                        //    }
                                        //case SDL.SDL_Keycode.SDLK_LEFT:
                                        //    {
                                        //        LEFT_KeyDownHandler();
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

            // Close application
            Environment.Exit(0);
        }

        #region Handlers
        int startX, startY;
        bool mouseDrag = false;
        bool animation = false;

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
            int fullWidth, fullHeight;
            SDL.SDL_GetWindowSize(window, out fullWidth, out fullHeight);

            if (mouseDrag)
            { 
                shape.yAngle += rt(startX - moveEvent.x, fullWidth);
                shape.xAngle += rt(startY - moveEvent.y, fullHeight);

                startX = moveEvent.x;
                startY = moveEvent.y;
            }
        }
        private Func<double, double, double> rt = (l, f) => (l / f * 8);

        private void Enter_KeyDownHandler(SDL.SDL_Event e)
        {
            var keyEvent = e.key;

            if (keyEvent.repeat != 0)
            {
                return;
            }

            animation = !animation;
            Console.WriteLine("E");
        }
        #endregion Handlers

        private void Draw()
        {
            context.UpdateParameters();
            context.Clear();

            SDL.SDL_SetRenderDrawColor(context.Render, 0, 0, 0, 0);

            //context.DrawDotedLine(10, 10, 30, 30);

            // Draw axis
            context.DrawLine(0, 0, 0, context.Height);
            context.DrawLine(context.Width, 0, 0, 0);

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
            shape = new SquareDonat(50);
        }
    }
}
