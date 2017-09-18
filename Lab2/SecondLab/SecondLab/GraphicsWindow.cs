using System;
using System.Threading;
using SDL2;


namespace SecondLab
{
    class GraphicsWindow
    {
        private Thread mainThread;
        private IntPtr renderer;
        private IntPtr window;
        private int windowWidth, windowHeight;
        private double scale;
        SDL.SDL_Point[] ArrPoint= new SDL.SDL_Point[5]; //new SDL.SDL_Point[5] ;
        int Nesting = 9; // Вложенность
        int Sides = 4;
        double a = 0.254;

        public GraphicsWindow(int width, int height)
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
            window = SDL.SDL_CreateWindow("Second Lab",
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
                ArrPoint[0].x = 100;
                ArrPoint[0].y = 100;
                ArrPoint[1].x = 100;
                ArrPoint[1].y = 500;
                ArrPoint[2].x = 500;
                ArrPoint[2].y = 500;
                ArrPoint[3].x = 500;
                ArrPoint[3].y = 100;
                ArrPoint[4].x = 100;
                ArrPoint[4].y = 100;
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
                                        ChangeParamA(0.002);
                                        break;
                                    }
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    {
                                        ChangeParamA(-0.002);
                                        break;
                                    }
                                case SDL.SDL_Keycode.SDLK_RIGHT:
                                    {
                                        ChangeParamNesting(1);
                                        break;
                                    }
                                case SDL.SDL_Keycode.SDLK_LEFT:
                                    {
                                        ChangeParamNesting(-1);
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
            SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
            SDL.SDL_RenderClear(renderer);
            SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);

            UpdateWindowTitle();
            UpdateWindowStat();

       

            SDL.SDL_RenderDrawLines(renderer, ArrPoint, 5);
            for (int i = 0; i< Nesting; i++)
            {
                NewPolygon();
                SDL.SDL_RenderDrawLines(renderer, ArrPoint, 5);
            }



            SDL.SDL_RenderPresent(renderer);
        }


        private void UpdateWindowTitle()
        {
            SDL.SDL_SetWindowTitle(window, $"Second Lab (a:,l:)");
        }

     
        private void UpdateWindowStat()
        {
            SDL.SDL_GetWindowSize(window, out windowWidth, out windowHeight);

           // scale = Math.Min(windowHeight, windowWidth) * 0.4 / (a + l);
        }

        private void NewPolygon()
        {
            for (int i = 0; i < Sides ; i++)
            {
                NewPoint(i);
            }
            ArrPoint[4] = ArrPoint[0];
        }

        private void NewPoint(int CurrentCount)
        {
            ArrPoint[CurrentCount].x = (int)((1 - a) * (double)(ArrPoint[CurrentCount].x) + a * (double)(ArrPoint[CurrentCount+1].x));
            ArrPoint[CurrentCount].y = (int)((1 - a) * (double)(ArrPoint[CurrentCount].y) + a * (double)(ArrPoint[CurrentCount + 1].y));
        }
        //Для улитки
        private void DrawNext()
        {
            int halfHeight = windowHeight / 2;
            int halfWidth = windowWidth / 2;
            SDL.SDL_RenderDrawLine(renderer, TrX(0), TrY(halfHeight), TrX(0), TrY(-halfHeight));
            SDL.SDL_RenderDrawLine(renderer, TrX(halfWidth), TrY(0), TrX(-halfWidth), TrY(0));
        }


        private void ChangeParamA(double delta)
        {
            if ((delta > 0) && (a<1))
                a = a + delta;
            if ((delta < 0) && (a > 0))
                a = a + delta;
        }

        private void ChangeParamNesting(int delta)
        {
            if (Nesting > 0) 
                Nesting = Nesting + delta;
            if ((Nesting == 0) && (delta > 0))
                Nesting = Nesting + delta;

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
