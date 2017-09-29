using System;
using System.Threading;
using SDL2;

namespace Lab3
{
    class MainWindow
    {
        bool quit;
        private Thread mainThread;
        private IntPtr window;
        private int windowWidth, windowHeight;
        private WindowContext context;

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
            window = SDL.SDL_CreateWindow("AKG Lab3",
                                          100, 100,
                                          windowWidth, windowHeight,
                                          SDL.SDL_WindowFlags.SDL_WINDOW_ALLOW_HIGHDPI |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE |
                                          SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            context = new WindowContext(window, (uint)windowWidth, (uint)windowHeight);

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
                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        {

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

            context.Close();
            SDL.SDL_DestroyWindow(window);
            SDL.SDL_Quit();
        }

        private void Draw()
        {
            context.UpdateParameters();
            context.Clear();

            SDL.SDL_SetRenderDrawColor(context.Render, 0, 0, 0, 0);

            SDL.SDL_RenderDrawLine(context.Render,
                                    context.TrX(0), context.TrY(0),
                                    context.TrX(500), context.TrY(50));


            context.RefreshWindow();
        }

        private void UpdateWindowTitle()
        {
            SDL.SDL_SetWindowTitle(window, $"AKG_3");
        }
    }
}
