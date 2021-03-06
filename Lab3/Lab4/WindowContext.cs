﻿using System;
using System.Collections.Generic;
using System.Linq;
using SDL2;
using SDLGeometry;
using SDLColor;

namespace SDLWindow
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
        private Zbufer zbufer;

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
            zbufer = new Zbufer(this, height, width);

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

        /// <summary>
        /// Get Z-bufer
        /// </summary>
        public Zbufer Zbufer => zbufer;
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
        /// Set context draw color
        /// </summary>
        /// <param name="r">Red</param>
        /// <param name="g">Gren</param>
        /// <param name="b">Blue</param>
        public void SetColor(byte r, byte g, byte b)
        {
            SDL.SDL_SetRenderDrawColor(winRender, r, g, b, 255);
        }

        public void SetColor(Color color)
        {
            SDL.SDL_SetRenderDrawColor(winRender, color.R, color.G, color.B, 255);
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
        /// Draw doted line on render with context cordinate
        /// </summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        public void DrawDotedLine(Point p1, Point p2, int lineSegmentLength = 2)
        {
            DrawDotedLine(p1.X, p1.Y, p2.X, p2.Y, lineSegmentLength);
        }

        /// <summary>
        /// Draw doted line on render with context cordinate
        /// </summary>
        /// <param name="x1">First point X position</param>
        /// <param name="y1">First point Y position</param>
        /// <param name="x2">Second point X position</param>
        /// <param name="y2">Second point Y position</param>
        public void DrawDotedLine(int x1, int y1, int x2, int y2, int lineSegmentLength = 2)
        {
            int length = lineSegmentLength;
            bool draw = true;

            var approximation = new BresenhamApproximation(x1, y1, x2, y2);

            foreach (var p in approximation)
            {
                if (length == 0)
                {
                    draw = !draw;
                    length = lineSegmentLength;
                }

                if (draw)
                {
                    DrawPoint(p);
                }

                length--;
            }
        }

        /// <summary>
        /// Draw line on render with context cordinate
        /// </summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        public void DrawLine(Point p1, Point p2)
        {
            DrawLine(p1.X, p1.Y, p2.X, p2.Y);
        }

        /// <summary>
        /// Draw line on render with context cordinate
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
        /// Draw point on render with context cordinate
        /// </summary>
        /// <param name="p">Painting point</param>
        public void DrawPoint(Point p)
        {
            DrawPoint(p.X, p.Y);
        }

        /// <summary>
        /// Draw point on render with context cordinate
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
            return (int)(windowWidth / 2.0 + x * scale);
        }

        /// <summary>
        /// Transform context Y position to window Y
        /// </summary>
        /// <param name="y">Context Y position</param>
        /// <returns>Window Y position</returns>
        public int TrY(double y)
        {
            return (int)(windowHeight / 2.0 - ( y) * scale);
        }

        /// <summary>
        /// Transform point in window coordinate to context cordinate
        /// </summary>
        /// <param name="p">Point in window cordinates</param>
        /// <returns>Point in context cordinates</returns>
        /// <remarks>!!!!!!!DEPRICATADE!!!!!!!</remarks>
        public Point TrWindowPoint(Point p)
        {
            return new Point(XrT(p.X), YrT(p.Y));
        }

        /// <summary>
        /// Transform window X position to context X
        /// </summary>
        /// <param name="x">Window X position</param>
        /// <returns>Context X position</returns>
        /// <remarks>!!!!!!!DEPRICATADE!!!!!!!</remarks>
        public int XrT(double x)
        {
            return (int)(contextWidth / 2.0 - (windowWidth / 2.0 - x) / scale);
        }

        /// <summary>
        /// Transform window Y position to context Y
        /// </summary>
        /// <param name="y">Window Y position</param>
        /// <returns>Context Y position</returns>
        /// <remarks>!!!!!!!DEPRICATADE!!!!!!!</remarks>
        public int YrT(double y)
        {
            return (int)(contextHeight / 2.0 - (windowHeight / 2.0 - (windowHeight - y)) / scale);
        }
        #endregion Transformations
    }
}
