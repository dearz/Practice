using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace ControlDef
{
    public class EllipesButton : Control
    {
        private bool _mouseEntered;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics grap = e.Graphics;
            SizeF size = grap.MeasureString(this.Text, this.Font);
            float x = (this.Width - size.Width) / 2;
            float y = (this.Height - size.Height) / 2;
            grap.DrawString(this.Text, this.Font, new SolidBrush(ForeColor), x, y);     
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics grap = pevent.Graphics;
            if (this.Parent != null)
                grap.FillRectangle(new SolidBrush(Parent.BackColor), 0, 0, this.Width, this.Height);

            grap.SmoothingMode = SmoothingMode.HighQuality;

            if (_mouseEntered && (MouseButtons & MouseButtons.Left) == MouseButtons.Left)
            {
                Color mouseColor = Color.FromArgb(120, BackColor);
                grap.FillEllipse(new SolidBrush(mouseColor), 0, 0, this.Width - 1, this.Height - 1);
            }
            else
                grap.FillEllipse(new SolidBrush(Parent.BackColor), 0, 0, this.Width - 1, this.Height - 1);


            if ((MouseButtons & MouseButtons.Left) != MouseButtons.Left)
            {
                if (_mouseEntered)
                {
                    Pen mouseEnterPen = new Pen(Color.Orange, 2);
                    grap.DrawEllipse(mouseEnterPen, 1, 1, this.Width - 3, this.Height - 3);
                    mouseEnterPen.Dispose();
                }
                else if (this.Focused)
                {
                    Pen focusePen = new Pen(Color.Blue, 2);
                    grap.DrawEllipse(focusePen, 1, 1, this.Width - 3, this.Height - 3);
                    focusePen.Dispose();
                }
            }

            if (this.Focused)
            {
                Pen focuseDotPen = new Pen(Color.Black);
                focuseDotPen.DashStyle = DashStyle.Dot;
                grap.DrawEllipse(focuseDotPen, 3, 3, this.Width - 7, this.Height - 7);
                focuseDotPen.Dispose();
            }

            grap.DrawEllipse(Pens.Black, 0, 0, this.Width - 1, this.Height - 1);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _mouseEntered = true;
            this.Refresh();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _mouseEntered = false;
            this.Refresh();
            base.OnMouseLeave(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.Refresh();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            this.Refresh();
            base.OnLostFocus(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Focus();
                this.Refresh();
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                this.Refresh();
            base.OnMouseUp(e);
        }

        protected override void OnClick(EventArgs e)
        {
            MouseEventArgs m = (MouseEventArgs)e;
            if (m.Button == MouseButtons.Left)
                base.OnClick(e);
        }
    }
}
