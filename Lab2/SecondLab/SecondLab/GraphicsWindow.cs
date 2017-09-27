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
        SDL.SDL_Point[] ArrPoint ;
        SDL.SDL_Point StarPoint;
        double Nesting = 4; // Вложенность
        double a =0.246; //(Math.Tan(Math.Abs(2*(3.14/4*6)))) / (Math.Tan(Math.Abs(2 * (3.14 / 4 * 6)))+1);
        int Sides = 4; //Количество сторон    
        int R = 250; //Радиус
        double angle;
        public Random Rand;

        public GraphicsWindow(int width, int height)
        {
            windowWidth = width;
            windowHeight = height;
            scale = 1;
            Rand = new Random();
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
           

            renderer = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            bool quit = false;
            while (!quit)
            {
                SDL.SDL_Event sdlEvent;
                SDL.SDL_PollEvent(out sdlEvent);

                StarPoint.x = 300;
                StarPoint.y = 300;

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
                                        ChangeParamNesting(0.2);
                                        break;
                                    }
                                case SDL.SDL_Keycode.SDLK_LEFT:
                                    {
                                        ChangeParamNesting(-0.2);
                                        break;
                                    }
                                case SDL.SDL_Keycode.SDLK_1:
                                    {
                                        ChangeParamSides(1);
                                        break;
                                    }
                                case SDL.SDL_Keycode.SDLK_2:
                                    {
                                        ChangeParamSides(-1);
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

            UpdateWindowTitle();// поправить
            UpdateWindowStat();

            MainCreate();
            byte[] color = new byte[3];

            SDL.SDL_RenderDrawLines(renderer, ArrPoint, Sides+1);
            for (int i = 0; i< Nesting; i++)
            {
                //Rand.NextBytes(color);
                //SDL.SDL_SetRenderDrawColor(renderer, color[0], color[1], color[2], 255);
                if ((i+1) == ((int)(10*a)))
                    SDL.SDL_SetRenderDrawColor(renderer, 255, 45, 201, 255);
                else
                    SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);
                NewPolygon();
                Line();
                //SDL.SDL_RenderDrawLines(renderer, ArrPoint, Sides+1);
            }



            SDL.SDL_RenderPresent(renderer);
        }

        private void Line()
        {
            

            for (int i = 0; i < Sides; i++)
            {
                //Изменение координат
                int dx = Math.Abs(ArrPoint[i].x - ArrPoint[i + 1].x);
                int dy = Math.Abs(ArrPoint[i].y - ArrPoint[i + 1].y);

                //Направление приращивания
                int sx = (ArrPoint[i + 1].x >= ArrPoint[i].x) ? (1) : (-1);
                int sy = (ArrPoint[i + 1].y >= ArrPoint[i].y) ? (1) : (-1);

                if (dy < dx)
                {
                    int d = dy *2 - dx;
                    int d1 = dy *2;
                    int d2 = (dy - dx) * 2;
                    SDL.SDL_RenderDrawPoint(renderer, ArrPoint[i].x , ArrPoint[i].y );
                    int x = ArrPoint[i].x + sx;
                    int y = ArrPoint[i].y;
                    for (int j = 1; j <=dx; j++)
                    {
                        if (d > 0)
                        {
                            d =d + d2;
                            y =y + sy;
                        }
                        else
                            d =d + d1;
                        SDL.SDL_RenderDrawPoint(renderer, x, y);
                        x =x + sx;
                    }
                }
                else
                {
                    int d = dx*2 - dy;
                    int d1 = dx*2;
                    int d2 = (dx - dy) * 2;
                    SDL.SDL_RenderDrawPoint(renderer, ArrPoint[i].x, ArrPoint[i].y);
                    int x = ArrPoint[i].x;
                    int y = ArrPoint[i].y + sy;
                    for (int j = 1; j<=dy; j++)
                    {
                        if (d > 0)
                        {
                            d =d + d2;
                            x =x+ sx;
                        }
                        else
                            d =d+ d1;
                        SDL.SDL_RenderDrawPoint(renderer, x, y);
                        y =y + sy;
                    }
                }

            }
        }

        private void MainCreate()
        {
            ArrPoint = new SDL.SDL_Point[Sides + 1];
            double z = 0;
            int i = 0;
            double angle = 360.0 / Sides;
            while (i < Sides )
            {
                ArrPoint[i].x = StarPoint.x + (int)((Math.Cos(z / 180 * Math.PI) * R));
                ArrPoint[i].y = StarPoint.y - (int)((Math.Sin(z / 180 * Math.PI) * R));
                z = z + angle;
                i++;
            }
            ArrPoint[Sides] = ArrPoint[0];
        }


        private void UpdateWindowTitle()
        {
            
            SDL.SDL_SetWindowTitle(window, $"Second Lab (u:{a},Angle: {angle},Nesting:{(int)Nesting},Sides{Sides})");
        }

     
        private void UpdateWindowStat()
        {
            SDL.SDL_GetWindowSize(window, out windowWidth, out windowHeight);
        }

        private void NewPolygon()
        {
            for (int i = 0; i < Sides ; i++)
            {
                NewPoint(i);
            }
            ArrPoint[Sides] = ArrPoint[0];
        }

        private void NewPoint(int CurrentCount)
        {
            ArrPoint[CurrentCount].x = (int)((1 - a) * (double)(ArrPoint[CurrentCount].x) + a * (double)(ArrPoint[CurrentCount+1].x));
            ArrPoint[CurrentCount].y = (int)((1 - a) * (double)(ArrPoint[CurrentCount].y) + a * (double)(ArrPoint[CurrentCount + 1].y));
        }

        private void ChangeParamA(double delta)
        {
            angle = Math.Atan((double)a / (1-a));
            double change = a + delta;
            if (!(change < 0) && !(change > 1))
                a = change;
            //if ((delta < 0) && (a > 0))
            //    a = a + delta;
        }

        private void ChangeParamNesting(double delta)
        {
            if (Nesting > 0) 
                Nesting = Nesting + delta;
            if (((Nesting == 0) || (Nesting < 0) ) && (delta > 0))
                Nesting = Nesting + delta;

        }

        private void ChangeParamSides(int delta)
        {
            if ((Sides > 3) && (delta < 0))
                Sides = Sides + delta;
            if (delta >0)
                Sides = Sides + delta;
        }
    }
}
