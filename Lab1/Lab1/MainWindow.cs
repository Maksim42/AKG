using System;
using System.Threading;
using SDL2;

namespace Lab1
{
    class MainWindow
    {
        private Thread mainThread;
        private IntPtr renderer;
        private IntPtr window;
        private int windowWidth, windowHeight;
        private double scale;
        private double timeStep;
        private int a;
        private int l;

        public MainWindow(int width, int height)
        {
            windowWidth = width;
            windowHeight = height;
            timeStep = 0.05;
            a = 7;
            l = 8;
            scale = 1;
            mainThread = new Thread(MainCycle);
        }

        public void Shown()
        {
            mainThread.Start();
            mainThread.Join();
        }

        private void MainCycle()
        {
            SDL.SDL_Init(SDL.SDL_INIT_EVERYTHING);
            window = SDL.SDL_CreateWindow("AKG Lab1",
                                          100, 100,
                                          windowWidth, windowHeight,
                                          SDL.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            bool quit = false;
            while (!quit)
            {
                SDL.SDL_Event sdlEvent;
                SDL.SDL_PollEvent(out sdlEvent);
                switch (sdlEvent.type)
                {
                    case SDL.SDL_EventType.SDL_QUIT:
                        {
                            quit = true;
                            break;
                        }
                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        {
                            var key = sdlEvent.key;
                            switch (key.keysym.sym)
                            {
                                case SDL.SDL_Keycode.SDLK_UP:
                                    {
                                        ChangeParamA(1);
                                        break;
                                    }
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    {
                                        ChangeParamA(-1);
                                        break;
                                    }
                                case SDL.SDL_Keycode.SDLK_RIGHT:
                                    {
                                        ChangeParamL(1);
                                        break;
                                    }
                                case SDL.SDL_Keycode.SDLK_LEFT:
                                    {
                                        ChangeParamL(-1);
                                        break;
                                    }
                            }
                            break;
                        }
                }
                Draw();
                Thread.Sleep(10);
            }

            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }

        private void Draw()
        {
            SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
            SDL.SDL_RenderClear(renderer);

            SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);

            UpdateWindowTitle();
            UpdateWindowStat();

            DrawAxis();

            var previousPoint = new SDL.SDL_Point();
            var nextPoint = new SDL.SDL_Point();

            previousPoint = CalculatePoint(0);

            for (double t = timeStep; t <= Math.PI * 2; t += timeStep)
            {
                nextPoint = CalculatePoint(t);

                SDL.SDL_RenderDrawLine(renderer,
                                       previousPoint.x, previousPoint.y,
                                       nextPoint.x, nextPoint.y);

                previousPoint = nextPoint;
            }

            SDL.SDL_RenderPresent(renderer);
        }

        private void UpdateWindowTitle()
        {
            SDL.SDL_SetWindowTitle(window, $"AKG Lab1 (a:{a},l:{l})");
        }

        private void UpdateWindowStat()
        {
            SDL.SDL_GetWindowSize(window, out windowWidth, out windowHeight);

            scale = (Math.Min(windowHeight, windowWidth) / 2) * 0.8 / (a + l);
        }

        private void DrawAxis()
        {
            int halfHeight = windowHeight / 2;
            int halfWidth = windowWidth / 2;
            int arrowSize = 4;

            SDL.SDL_RenderDrawLine(renderer, TrX(0), TrY(halfHeight), TrX(0), TrY(-halfHeight));
            SDL.SDL_RenderDrawLine(renderer, TrX(halfWidth), TrY(0), TrX(-halfWidth), TrY(0));

            SDL.SDL_Point[] arrow = new SDL.SDL_Point[3];

            arrow[0].x = TrX(-arrowSize);
            arrow[0].y = TrY(halfHeight - 2 * arrowSize);
            arrow[1].x = TrX(0);
            arrow[1].y = TrY(halfHeight);
            arrow[2].x = TrX(arrowSize);
            arrow[2].y = TrY(halfHeight - 2 * arrowSize);
            SDL.SDL_RenderDrawLines(renderer, arrow, arrow.Length);

            arrow[0].x = TrX(halfWidth  - 2 * arrowSize);
            arrow[0].y = TrY(-arrowSize);
            arrow[1].x = TrX(halfWidth);
            arrow[1].y = TrY(0);
            arrow[2].x = TrX(halfWidth - 2 * arrowSize);
            arrow[2].y = TrY(arrowSize);
            SDL.SDL_RenderDrawLines(renderer, arrow, arrow.Length);
        }

        private SDL.SDL_Point CalculatePoint(double t)
        {
            var point = new SDL.SDL_Point();
            point.x = TrX((a * Math.Pow(Math.Cos(t), 2) + l * Math.Cos(t)) * scale);
            point.y = TrY((a * Math.Cos(t) * Math.Sin(t) + l * Math.Sin(t)) * scale);

            return point;
        }

        private void ChangeParamA(int delta)
        {
            int changed = a + delta;
            if (changed <= int.MaxValue && changed > 0)
            {
                a = changed;
            }
        }

        private void ChangeParamL(int delta)
        {
            int changed = l + delta;
            if (changed <= int.MaxValue && changed > 0)
            {
                l = changed;
            }
        }

        #region PositionTransforms
        private int TrX(double x)
        {
            return (int)(windowWidth / 2 + x);
        }

        private int TrY(double y)
        {
            return (int)(windowHeight / 2 - y);
        }
        #endregion PositionTransforms
    }
}
