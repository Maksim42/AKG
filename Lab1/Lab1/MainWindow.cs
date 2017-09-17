using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SDL2;

namespace Lab1
{
    class MainWindow
    {
        private Thread mainThread;
        private IntPtr renderer;
        private int windowWidth;
        private int windowHeight;

        public MainWindow(int width, int height)
        {
            windowWidth = width;
            windowHeight = height;
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
            IntPtr wnd = SDL.SDL_CreateWindow("AOKG Lab 1",
                                               100, 100,
                                               windowWidth, windowHeight,
                                               /*SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE |*/
                                               SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            //  var shape = new Shape();

            renderer = SDL.SDL_CreateRenderer(wnd, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

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
                                case SDL.SDL_Keycode.SDLK_DOWN:
                                    // do smth
                                    break;
                                case SDL.SDL_Keycode.SDLK_UP:
                                    // do smth
                                    break;
                            }
                            break;
                        }
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        {
                            if (sdlEvent.button.button == SDL.SDL_BUTTON_LEFT)
                            {
                                // do smth
                            }
                            else
                            if (sdlEvent.button.button == SDL.SDL_BUTTON_RIGHT)
                            {
                                // do smth
                            }
                            break;
                        }

                }
                Draw();
                Thread.Sleep(10); // somehow calibrate render loop
            }
            SDL.SDL_DestroyRenderer(renderer);
            SDL.SDL_DestroyWindow(wnd);
            SDL.SDL_Quit();
        }

        private void Draw()
        {
            SDL.SDL_SetRenderDrawColor(renderer, 255, 255, 255, 255);
            SDL.SDL_RenderClear(renderer);
                   
            SDL.SDL_SetRenderDrawColor(renderer, 0, 0, 0, 0);

            var axisPoint = new SDL.SDL_Point[2];
            axisPoint[0].x = windowWidth / 2;
            axisPoint[0].y = 0;
            axisPoint[1].x = axisPoint[0].x;
            axisPoint[1].y = windowHeight;

            SDL.SDL_RenderDrawLines(renderer, axisPoint, axisPoint.Length);

            int pc = windowWidth;
            var points = new SDL.SDL_Point[pc];

            for (int pi = 0; pi < points.Length; pi++)
            {
                points[pi].x = pi;
                points[pi].y = windowHeight/2 - (int)(Math.Sin(pi / 200) * 30);
            }

            SDL.SDL_RenderDrawPoints(renderer, points, points.Length);

            SDL.SDL_RenderPresent(renderer);
        }
    }
}
