using System;
using System.Threading;
using SDL2;

namespace Lab3
{
    class MainWindow
    {
        bool quit;
        private Thread mainThread;
        private IntPtr renderer;
        private IntPtr window;
        private int windowWidth, windowHeight;
        private double scale;
        

        public MainWindow(int width, int height)
        {
            quit = false;

            windowWidth = width;
            windowHeight = height;
           
            scale = 1;
            mainThread = new Thread(MainCycle);
        }

        public void Shown()
        {
            mainThread.Start();
            //mainThread.Join();
        }

        public void Close()
        {
            quit = true;
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
                    //case SDL.SDL_EventType.SDL_KEYDOWN:
                    //    {
                    //        var key = sdlEvent.key;
                    //        switch (key.keysym.sym)
                    //        {
                    //            case SDL.SDL_Keycode.SDLK_UP:
                    //                {
                    //                    ChangeParamA(1);
                    //                    break;
                    //                }
                                
                    //        }
                    //        break;
                    //    }
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




            SDL.SDL_RenderPresent(renderer);
        }

        private void UpdateWindowTitle()
        {
            SDL.SDL_SetWindowTitle(window, $"AKG_3");
        }

        private void UpdateWindowStat()
        {
            SDL.SDL_GetWindowSize(window, out windowWidth, out windowHeight);

            scale = (double)(Math.Min(windowHeight, windowWidth) / 2) / 500;
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

        private int XrT(int x)
        {
            return x - windowWidth / 2;
        }

        private int YrT(int y)
        {
            return windowHeight / 2 - y;
        }
        #endregion PositionTransforms
    }
}
