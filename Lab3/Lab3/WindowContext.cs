using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Lab3
{
    class WindowContext
    {
        private IntPtr window;
        private IntPtr winRender;
        private uint contextWidth;
        private uint contextHeight;
        private int windowWidth;
        private int windowHeight;
        private double scale;

        /// <summary>
        /// Create new WindowContext for window
        /// </summary>
        /// <param name="window">Window handler</param>
        /// <param name="width">Context width</param>
        /// <param name="height">Context height</param>
        public WindowContext(IntPtr window, uint width, uint height)
        {
            this.window = window;
            contextWidth = width;
            contextHeight = height;

            winRender = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
        }

        public IntPtr Render => winRender;

        /// <summary>
        /// Fill all the window with white color
        /// </summary>
        public void Clear()
        {
            SDL.SDL_SetRenderDrawColor(winRender, 255, 255, 255, 255);
            SDL.SDL_RenderClear(winRender);
        }

        /// <summary>
        /// Update context parameters for curent window state 
        /// </summary>
        public void UpdateParameters()
        {
            SDL.SDL_GetWindowSize(window, out windowWidth, out windowHeight);

            if (windowHeight < windowWidth)
            {
                scale = (double)windowHeight / contextHeight;
            }
            else
            {
                scale = (double)windowWidth / contextWidth;
            }
        }

        /// <summary>
        /// Redraw window context
        /// </summary>
        public void RefreshWindow()
        {
            SDL.SDL_RenderPresent(winRender);
        }

        /// <summary>
        /// Close all open context handlers
        /// </summary>
        public void Close()
        {
            SDL.SDL_DestroyRenderer(winRender);
        }

        #region Transformations
        /// <summary>
        /// Transform context X position to window X
        /// </summary>
        /// <param name="x">Context X position</param>
        /// <returns>Window X position</returns>
        public int TrX(double x)
        {
            return (int)(windowWidth / 2.0 - (contextWidth / 2.0 - x) * scale);
        }

        /// <summary>
        /// Transform context Y position to window Y
        /// </summary>
        /// <param name="y">Context Y position</param>
        /// <returns>Window Y position</returns>
        public int TrY(double y)
        {
            return windowHeight - (int)(windowHeight / 2.0 - (contextHeight / 2.0 - y) * scale);
        }
        #endregion Transformations
    }
}
