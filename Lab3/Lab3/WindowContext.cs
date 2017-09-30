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
        /// <summary>
        /// Window attached to this context
        /// </summary>
        private IntPtr window;
        /// <summary>
        /// SDL render for this context
        /// </summary>
        private IntPtr winRender;
        private int contextWidth;
        private int contextHeight;
        private int windowWidth;
        private int windowHeight;
        private double scale;

        /// <summary>
        /// Create new WindowContext for window
        /// </summary>
        /// <param name="window">Window handler</param>
        /// <param name="width">Context width</param>
        /// <param name="height">Context height</param>
        public WindowContext(IntPtr window, int width, int height)
        {
            this.window = window;
            contextWidth = Math.Abs(width);
            contextHeight = Math.Abs(height);

            winRender = SDL.SDL_CreateRenderer(window, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
        }

        #region Propertys
        /// <summary>
        /// Get SDL render curent context
        /// </summary>
        public IntPtr Render => winRender;

        /// <summary>
        /// Get context width
        /// </summary>
        public int Width => contextWidth;

        /// <summary>
        /// Get context height
        /// </summary>
        public int Height => contextHeight;
        #endregion Propertys

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

            double scaleX = (double)windowWidth / contextWidth;
            double scaleY = (double)windowHeight / contextHeight;

            scale = Math.Min(scaleX, scaleY);
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

        #region Painting
        /// <summary>
        /// Draw line on render with context cordinate transforms
        /// </summary>
        /// <param name="x1">First point X position</param>
        /// <param name="y1">First point Y position</param>
        /// <param name="x2">Second point X position</param>
        /// <param name="y2">Second point Y position</param>
        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            SDL.SDL_RenderDrawLine(winRender,
                                   TrX(x1), TrY(y1),
                                   TrX(x2), TrY(y2));
        }

        /// <summary>
        /// Draw point on render with context cordinate transforms
        /// </summary>
        /// <param name="x">Point X position</param>
        /// <param name="y">Point Y position</param>
        public void DrawPoint(int x, int y)
        {
            SDL.SDL_RenderDrawPoint(winRender,
                                    TrX(x), TrY(y));
        }
        #endregion Painting

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
