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
        int a = 7;
        int l = 8;

        public MainWindow(int width, int height)
        {
            windowWidth = width;
            windowHeight = height;
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
            //  var shape = new Shape();

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
                                case SDL.SDL_Keycode.SDLK_i:
                                    // TODO: invert color
                                    break;
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
            SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
            SDL.SDL_RenderClear(renderer);

            SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);

            UpdateWindowTitle();
            UpdateWindowStat();

            DrawAxis();

            var previousPoint = new SDL.SDL_Point();
            var nextPoint = new SDL.SDL_Point();

            previousPoint = CalculatePoint(0);

            for (double t = 0; t <= Math.PI * 2; t += 0.05)
            {
                nextPoint = CalculatePoint(t);

                SDL.SDL_RenderDrawLines(renderer,
                                    new [] { previousPoint, nextPoint },
                                    2);

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

            scale = Math.Min(windowHeight, windowWidth) * 0.4 / (a + l);
        }

        private void DrawAxis()
        {
            int halfHeight = windowHeight / 2;
            int halfWidth = windowWidth / 2;
            SDL.SDL_RenderDrawLine(renderer, TrX(0), TrY(halfHeight), TrX(0), TrY(-halfHeight));
            SDL.SDL_RenderDrawLine(renderer, TrX(halfWidth), TrY(0), TrX(-halfWidth), TrY(0));
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
            return TrX((int)x);
        }

        private int TrY(double y)
        {
            return TrY((int)y);
        }

        private int TrX(int x)
        {
            return windowWidth / 2 + x;
        }

        private int TrY(int y)
        {
            return windowHeight / 2 - y;
        }
        #endregion PositionTransforms
    }
}
