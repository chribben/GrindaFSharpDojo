open System
open System.Windows.Forms
open System.Drawing
let cellSide = 10
let noOfCellsInOneDir = 30 
let side = cellSide * noOfCellsInOneDir
let totNoOfCells = pown noOfCellsInOneDir 2


//*************VIEW************
let form = new Form()
let keypressEventHandler (sender:Object) (e:KeyPressEventArgs -> unit) = ()
form.ClientSize <- new Size(side, side)
form.FormBorderStyle <- FormBorderStyle.FixedSingle;
form.MaximizeBox <- false;
form.MinimizeBox <- false;
form.Show() |> ignore
let graphics = form.CreateGraphics()
graphics.Clear(Color.White)
let tupleToPoint (x,y) = new Point(cellSide*x,cellSide*y)
let black = new SolidBrush(Color.Black)
let white = new SolidBrush(Color.White)
//graphics.FillRectangle(black, new Rectangle(0,0,cellSide, cellSide)) 

//*************MODEL*******************

//*************UPDATE******************
