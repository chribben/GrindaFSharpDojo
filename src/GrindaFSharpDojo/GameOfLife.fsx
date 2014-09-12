open System
open System.Windows.Forms
open System.Drawing
let cellSide = 10
let noOfCellsInOneDir = 16 
let side = cellSide * noOfCellsInOneDir
let totNoOfCells = pown noOfCellsInOneDir 2

//*************MODEL*******************
type State = Alive | Dead
type Cell = {state: State; coords: (int*int)}
type Game = {board: Cell list list}

//*************UPDATE******************
let getCell game (x,y) = game.board.[x].[y]
let getNeighbours game cell = 
    let (x,y) = cell.coords
    let norm p = (p + noOfCellsInOneDir)%noOfCellsInOneDir
    List.collect (fun x -> List.map (fun y -> getCell game (x,y)) [norm(y-1);y;norm(y+1)]) [norm(x-1);x;norm(x+1)] |>
    List.filter (fun c -> c.coords <> (x,y))
let noOfLiveNeighbours game cell = getNeighbours game cell |> List.sumBy (fun cell -> if cell.state = Alive then 1 else 0)
let updateCell game cell = match noOfLiveNeighbours game cell with
                            | 3 when cell.state = Dead -> {cell with state = Alive}
                            | 2 | 3 -> cell
                            | n when n < 2 || n > 3 -> {cell with state = Dead}
let updateGame game = {game with board = List.map (fun col -> col |> List.map (fun cell -> updateCell game cell)) game.board}
//***************INIT******************
let rnd = new Random()
let game = {board = 
            List.map (fun row -> 
                List.map (fun col -> 
                    {state = (if rnd.Next() % 2 = 0 then Dead else Alive); coords = (row,col)}) [0..noOfCellsInOneDir-1]) 
                        [0..noOfCellsInOneDir-1]}

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
let toRect cell = new Rectangle(tupleToPoint cell.coords, new Size(cellSide,cellSide))
let toRectsArray cells = cells |> List.map (fun cell -> toRect cell) |> List.toArray

let rec updateBoard game = async{
        let aliveCells = game.board |> List.collect (fun r -> r) |> List.filter (fun cell -> cell.state = Alive)
        if List.length aliveCells = 0 then 
            Async.Sleep 0 |> ignore
        else
            let (aliveCells, deadCells) = game.board |> List.collect (fun r -> r ) |> List.partition (fun cell -> cell.state = Alive)
            graphics.FillRectangles(black, aliveCells |> toRectsArray)
            graphics.FillRectangles(white, deadCells |> toRectsArray)
            do! Async.Sleep 1000
            do! updateBoard (updateGame game)
    }
Async.StartImmediate(updateBoard game)