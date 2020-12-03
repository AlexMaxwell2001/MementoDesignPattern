using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
//OS:Windows , IDE:VS Code 1.51.0

/*PUML Class diagram 
    You can preview classDiag.puml for this. */

/*I Implemented the Memento Design pattern for my undo-redo functionality.

  A brief description of the code: I have three classes which are associated with the design patten, User, Canvas and Memento classes.
  When a user adds a shape to the canvas the user .Action() method is called, which takes a snapshot of the previous state before adding and puts it on the undo Stack.
  This is done by putting the list of shapes from the canvas into a Memento object which is put on the stack.
  When the user uses the undo functionality the last/previous state in a Memento object is popped off the undo stack and made the new canvas, the old canvas is then put on the redo stack.
  When the user uses the redo functionality the overwritten state in a Memento object is popped off the redo stack and made the new canvas, the old canvas is then put on the undo stack.

  Comparision between the two design patterns in both my assignments:
  I used Command design pattern in my last assignment and Memento Design pattern in this assignment. Comparing both design patters on the difficulty to implement.
  The Command Design pattern is much easier to develop than the Memento Design pattern, the Memento pattern takes alot of understanding pre-implementation compared to the Command pattern.
  I feel the Memento pattern is much better in this specific program, there's no need for the amount of code and Object creation that comes with the Command Pattern, with the
  Memento pattern there is a lot less code and it's more neater even though it's much more complex.The Memento pattern for this program and all programs, generally would scale much better,
  due to less code, less object creation (Command makes an object each time a shape is added), and with the Command pattern it's less dynamic then the Memento pattern for the undo-redo 
  functionality as it can only more undo one at a time to get where it wants to be but with Memento there is the ability to choose the state immediately. For this assignment it would be desirable 
  to dynamic choose what stage to undo and redo to although not implemented, Command wouldn't have the capabilities to do this.
  */

  /*Personal Challenges of designing with different design patterns: Personally I found the Command pattern easy to implement, So when starting this assignment I thought it would be similiar complexity,
  but Memento was very challenging to implement even though I knew the code, algorithms, the idea behind design pattern and how to work with data structures. To get this design pattern to work took
  much longer than I would've wanted, So I question whether in future developing with design patterns like these would it be better to work with the easier to implement pattern and have an overhead.
  Designing with both design patterns on the same program was interesting as I faced challenges in somewhat forgetting the way I implemented it in the previous Assignment to be able to implement with this design pattern.
  When developing the solution I found it hard to not fall back on the already working ideas and methadolgies of Command, but I managed to put what I knew aside and develop a completely  different solution and design pattern*/

    class Program{
        public static void Main()
        {
            Canvas previousCanvas=new Canvas();
            Canvas temp=new Canvas();
            Stack<string> previous = new Stack<string>();
            string filePath=@".\svg.svg";
            string svg="</svg>";
            string nothing="";
            File.WriteAllText(filePath,nothing);
            Random rnd = new Random();
            Canvas canvas = new Canvas();
            Console.WriteLine(canvas);
            User user = new User();
            while(true){
                string input=Console.ReadLine();
                if(input.ToUpper()=="Q")break;
                string [] ar= input.Split(" ");
                string first = ar[0];
                if(first.ToUpper().Equals("A")){
                    string second =ar[1];
                    if(second.Equals("Rectangle")||second.Equals("rectangle")){
                        if(ar.Length>2){
                            string [] tempo=input.Split("(");string tem= tempo[1].Replace(")","");string [] newar= tem.Split(",");  
                            user.Action(new Rectangle(int.Parse(newar[0]), int.Parse(newar[1]), int.Parse(newar[2]), int.Parse(newar[3]),rnd.Next(1, 10000)), canvas);
                        }
                        else user.Action(new Rectangle(rnd.Next(1, 200), rnd.Next(1, 200), rnd.Next(1, 200), rnd.Next(1,200),rnd.Next(1, 10000)), canvas);
                    }else if(second.Equals("Circle")||second.Equals("circle")){
                        if(ar.Length>2){
                            string [] tempo=input.Split("(");string tem= tempo[1].Replace(")","");string [] newar= tem.Split(",");  
                            user.Action(new Circle(int.Parse(newar[0]), int.Parse(newar[1]), int.Parse(newar[2]),rnd.Next(1, 10000)), canvas);
                        }
                        else user.Action(new Circle(rnd.Next(1, 200), rnd.Next(1, 200), rnd.Next(1, 200),rnd.Next(1, 10000)), canvas);
                    }else if(second.Equals("Ellipse")||second.Equals("ellipse")){
                        if(ar.Length>2){
                            string [] tempo=input.Split("(");string tem= tempo[1].Replace(")","");string [] newar= tem.Split(",");  
                            user.Action(new Ellipse(int.Parse(newar[0]), int.Parse(newar[1]), int.Parse(newar[2]), int.Parse(newar[3]),rnd.Next(1, 10000)), canvas);
                        }
                        else user.Action(new Ellipse(rnd.Next(1, 200), rnd.Next(1, 200), rnd.Next(1, 200),rnd.Next(1,200),rnd.Next(1, 10000)), canvas);
                    }else if(second.Equals("Line")||second.Equals("line")){
                        if(ar.Length>2){
                            string [] tempo=input.Split("(");string tem= tempo[1].Replace(")","");string [] newar= tem.Split(",");  
                            user.Action(new Line(int.Parse(newar[0]), int.Parse(newar[1]), int.Parse(newar[2]), int.Parse(newar[3]),rnd.Next(1, 10000)), canvas);
                        }
                        else user.Action(new Line(rnd.Next(1, 200), rnd.Next(1, 200), rnd.Next(1, 200),rnd.Next(1,200),rnd.Next(1, 10000)), canvas);
                    }else if(second.Equals("Polyline")||second.Equals("polyline")){
                        if(ar.Length>2){
                            string [] tempo=input.Split("(");string tem= tempo[1].Replace(")","");string [] newar= tem.Split(",");
                            string pointsString=getString(newar);
                            user.Action(new Polyline(pointsString,rnd.Next(1, 10000)), canvas);
                        }
                        else user.Action(new Polyline(rnd.Next(1, 200)+", "+ rnd.Next(1, 200)+" "+ rnd.Next(1, 200)+", " +rnd.Next(1,200),rnd.Next(1, 10000)), canvas);
                    }else if(second.Equals("Polygon")||second.Equals("polygon")){
                        if(ar.Length>2){
                            string [] tempo=input.Split("(");string tem= tempo[1].Replace(")","");string [] newar= tem.Split(",");
                            string pointsString=getString(newar);
                            user.Action(new Polygon(pointsString,rnd.Next(1, 10000)), canvas);
                        }
                        else user.Action(new Polygon(rnd.Next(1, 200)+", "+ rnd.Next(1, 200)+" "+ rnd.Next(1, 200)+", " +rnd.Next(1,200)+ " "+ rnd.Next(1,200)+ ", " + rnd.Next(1,200),rnd.Next(1, 10000)), canvas);
                    }else if(second.Equals("Path")||second.Equals("path")){
                        if(ar.Length>2){
                            string [] tempo=input.Split("(");string tem= tempo[1].Replace(")","");string [] newar= tem.Split(",");
                            string pointsString=getString(newar);
                            user.Action(new Path(pointsString,rnd.Next(1, 10000)), canvas);
                        }
                        else user.Action(new Path("M"+rnd.Next(1, 200)+","+ rnd.Next(1, 200)+" "+ "Q"+rnd.Next(1, 200)+"," +rnd.Next(1,200)+" "+rnd.Next(1,200)+ ","+rnd.Next(1,200)+" T"+rnd.Next(1,200),rnd.Next(1, 10000)), canvas);
                    }else if(second.Equals("Text")||second.Equals("text")){
                        string [] textar={"this is new", "different text", "Randomness", "What!", "Filling the array with random text","This is a sample text", "This is the last index of the array"};
                        if(ar.Length>2){
                            string [] tempo=input.Split("(");string tem= tempo[1].Replace(")","");string [] newar= tem.Split(",");
                            user.Action(new WordMaker(int.Parse(newar[0]), int.Parse(newar[1]), newar[2],rnd.Next(1, 10000)), canvas);
                        }
                        else user.Action(new WordMaker(rnd.Next(1, 200),rnd.Next(1,200), textar[rnd.Next(0,7)],rnd.Next(1, 10000)), canvas);
                    }else{
                        Console.WriteLine("The shape " + second + " doesn't exist!");
                    }
                }else if(first.ToUpper().Equals("U")){
                    user.Undo(canvas);
                }else if(first.ToUpper().Equals("UN")){
                    canvas = temp;
                    Console.WriteLine("The Clear command has been undone!");
                }else if(first.ToUpper().Equals("R")){
                    user.Redo(canvas);
                }else if(first.ToUpper().Equals("H")){
                    commandPrintout();
                }else if(first.ToUpper().Equals("D")){
                    Console.WriteLine(canvas);
                }else if(first.ToUpper().Equals("P")){
                    if(previous.Count>0){
                        previous.Pop();
                        string var= previous.Pop();
                        File.WriteAllText(filePath,var);
                        string [] tempar= File.ReadAllLines(filePath);
                        string canvasstring = "<svg width="+@"""" +400+@""""+" height="+ @""""+400+@""""+ " version="+ @""""+1.1+@""""+ " xmlns="+@""""+ "http:"+ "//www.w3.org/2000/svg"+ @""""+">"+ Environment.NewLine +svg;
                        File.WriteAllText(filePath,canvasstring);
                        Save(tempar);
                        Console.WriteLine("Previously Saved canvas loaded to svg.svg File");
                    }else{
                        Console.WriteLine("No Previously saved files were found");
                    }
                }else if(first.ToUpper().Equals("S")){
                    if(canvas.ToString().Length>0){
                        string var= canvas.ToString();
                        File.WriteAllText(filePath,var);
                        string [] tempar= File.ReadAllLines(filePath);
                        string canvasstring = "<svg width="+@"""" +400+@""""+" height="+ @""""+400+@""""+ " version="+ @""""+1.1+@""""+ " xmlns="+@""""+ "http:"+ "//www.w3.org/2000/svg"+ @""""+">"+ Environment.NewLine +svg;
                        File.WriteAllText(filePath,canvasstring);
                        Save(tempar);
                        previous.Push(var);
                    }
                }else if(first.ToUpper().Equals("C")){
                    temp = canvas;
                    canvas= new Canvas();
                    Console.WriteLine(canvas);
                }
                else{
                    Console.WriteLine("The command " + first+ " adoesn't exist, you can use the H command to see available commands");
                }
            }
            Console.WriteLine("Goodbye!");
        }
        public static string RemoveSpaces(string value)
        {
            return Regex.Replace(value, @"\s+", " ");
        }
        public static void Save(string [] tempar){
            for(int i=2;i<tempar.Length;i++){
                if(tempar[i].Contains(":")){
                    string [] temp= tempar[i].Split(" (");string [] temp2= tempar[i].Split(":");
                    string shape=temp[0].Replace(">","");string shapes=RemoveSpaces(shape);
                    if(shapes.Equals(" Rectangle")){
                        string [] ar1=temp2[1].Split(",");string id=ar1[0].Replace(" ","");
                        string [] ar2=temp2[2].Split(",");string x=ar2[0].Replace(" ","");
                        string [] ar3=temp2[3].Split(",");string y=ar3[0].Replace(" ","");
                        string [] ar4=temp2[4].Split(",");string width=ar4[0].Replace(" ","");
                        string [] ar5=temp2[5].Split(")");string height=ar5[0].Replace(" ","");
                        addShape(x,y,width,height,shapes,id);
                    }else if(shapes.Equals(" Circle")){
                        string [] ar1=temp2[1].Split(",");string id=ar1[0].Replace(" ","");
                        string [] ar2=temp2[2].Split(",");string cx=ar2[0].Replace(" ","");
                        string [] ar3=temp2[3].Split(",");string cy=ar3[0].Replace(" ","");
                        string [] ar4=temp2[4].Split(")");string r=ar4[0].Replace(" ","");
                        addShape(cx,cy,r,null,shapes,id);
                    }else if(shapes.Equals(" Ellipse")){
                        string [] ar1=temp2[1].Split(",");string id=ar1[0].Replace(" ","");
                        string [] ar2=temp2[2].Split(",");string cx=ar2[0].Replace(" ","");
                        string [] ar3=temp2[3].Split(",");string cy=ar3[0].Replace(" ","");
                        string [] ar4=temp2[4].Split(",");string rx=ar4[0].Replace(" ","");
                        string [] ar5=temp2[5].Split(")");string ry=ar5[0].Replace(" ","");
                        addShape(cx,cy,rx,ry,shapes,id);
                    }else if(shapes.Equals(" Line")){
                        string [] ar1=temp2[1].Split(",");string id=ar1[0].Replace(" ","");
                        string [] ar2=temp2[2].Split(",");string x1=ar2[0].Replace(" ","");
                        string [] ar3=temp2[3].Split(",");string x2=ar3[0].Replace(" ","");
                        string [] ar4=temp2[4].Split(",");string y1=ar4[0].Replace(" ","");
                        string [] ar5=temp2[5].Split(")");string y2=ar5[0].Replace(" ","");
                        addShape(x1,x2,y1,y2,shapes,id);
                    }else if(shapes.Equals(" Polyline")){
                        string [] ar1=temp2[1].Split(",");string id=ar1[0].Replace(" ","");
                        string [] ar2=temp2[2].Split(")");string points=ar2[0];
                        addShape(points,null,null,null,shapes, id);
                    }else if(shapes.Equals(" Polygon")){
                        string [] ar1=temp2[1].Split(",");string id=ar1[0].Replace(" ","");
                        string [] ar2=temp2[2].Split(")");string points=ar2[0];
                        addShape(points,null,null,null,shapes,id);
                    }else if(shapes.Equals(" Path")){
                        string [] ar1=temp2[1].Split(",");string id=ar1[0].Replace(" ","");
                        string [] ar2=temp2[2].Split(")");string d=ar2[0];
                        addShape(d,null,null,null,shapes,id);
                    }else if(shapes.Equals(" Text")){
                        string [] ar1=temp2[1].Split(",");string id=ar1[0].Replace(" ","");
                        string [] ar2=temp2[2].Split(",");string x=ar2[0].Replace(" ","");
                        string [] ar3=temp2[3].Split(",");string y=ar3[0].Replace(" ","");
                        string [] ar4=temp2[4].Split(")");string info=ar4[0];
                        addShape(x,y,info,null,shapes,id);
                    }
                }     
            }
        }
        public static void commandPrintout(){
            Console.WriteLine(("Commands:"));
            Console.WriteLine("\tH\t\t\tHelp - displays this message");
            Console.WriteLine("\tA <shape>\t\tAdd <shape> to the canvas");
            Console.WriteLine("\tA <shape> (dimensions)  Add <shape> to the canvas with user defined dimensions seperated by ','");
            Console.WriteLine("\tU\t\t\tUndo last operation");
            Console.WriteLine("\tR\t\t\tRedo last operation");
            Console.WriteLine("\tP\t\t\tLoad previously saved file");
            Console.WriteLine("\tC\t\t\tClear canvas");
            Console.WriteLine("\tUN\t\t\tUndo a clear canvas");
            Console.WriteLine("\tD\t\t\tDisplays the canvas to the console");
            Console.WriteLine("\tS\t\t\tSave your canvas to a svg file");
            Console.WriteLine("\tQ\t\tQuit application");
        }
        public static void addShape(string value1,string value2,string value3,string value4,string shape,string id)
        {
            string filePath=@".\svg.svg";
            string svg="</svg>";
            string lines=File.ReadAllText(filePath);
            readWrite();
            if(shape.Equals(" Rectangle")){
                using (var writer = File.AppendText(filePath))  
                {  
                    writer.Write("\t"+"<rect"+ " id="+@""""+id+@""""+" x="+@""""+value1+@""""+" y="+@""""+value2+@""""+" width="+@""""+value3+@""""+" height="+@""""+value4+@""""+"/>" + Environment.NewLine);
                    writer.Write(svg);
                } 
            }else if(shape.Equals(" Circle")){
                using (var writer = File.AppendText(filePath))  
                {  
                    writer.Write("\t" +"<circle"+ " id="+@""""+id+@""""+" cx="+@""""+value1+@""""+" cy="+@""""+value2+@""""+" r="+@""""+value3+@""""+"/>" + Environment.NewLine);
                    writer.Write(svg);
                } 
            }else if(shape.Equals(" Ellipse")){
                using (var writer = File.AppendText(filePath))  
                {  
                    writer.Write("\t" +"<ellipse "+ " id="+@""""+id+@""""+" cx="+@""""+value1+@""""+" cy="+@""""+value2+@""""+" rx="+@""""+value3+@""""+ " ry="+@""""+value4+@""""+"/>" + Environment.NewLine);
                    writer.Write(svg);
                } 
            }else if(shape.Equals(" Line")){
                using (var writer = File.AppendText(filePath))  
                {  
                    writer.Write("\t" +"<line "+ " id="+@""""+id+@""""+" x1="+@""""+value1+@""""+" x2="+@""""+value2+@""""+" y1="+@""""+value3+@""""+ " y2="+@""""+value4+@""""+"/>" + Environment.NewLine);
                    writer.Write(svg);
                } 
            }else if(shape.Equals(" Polyline")){
                using (var writer = File.AppendText(filePath))  
                {  
                    writer.Write("\t" +"<polyline "+ " id="+@""""+id+@""""+" points="+@""""+value1+@""""+ "/>" +Environment.NewLine);
                    writer.Write(svg);
                } 
            }else if(shape.Equals(" Polygon")){
                using (var writer = File.AppendText(filePath))  
                {  
                    writer.Write("\t" +"<polygon "+ " id="+@""""+id+@""""+" points="+@""""+value1+@""""+ "/>" +Environment.NewLine);
                    writer.Write(svg);
                } 
            }else if(shape.Equals(" Path")){
                using (var writer = File.AppendText(filePath))  
                {  
                    writer.Write("\t" +"<path "+ " id="+@""""+id+@""""+" d="+@""""+value1+@""""+ "/>" +Environment.NewLine);
                    writer.Write(svg);
                } 
            }else if(shape.Equals(" Text")){
                using (var writer = File.AppendText(filePath))  
                {  
                    writer.Write("\t" +"<text"+ " id="+@""""+id+@""""+" x="+@""""+value1+@""""+" y="+@""""+value2+@""""+">"+value3 + "</text>" + Environment.NewLine);
                    writer.Write(svg);
                } 
            }
        }
        public static void readWrite(){
            //File path
            string filePath=@".\svg.svg";
            //Closing tag
            string svg="</svg>";
            string text;
            string value="";
            StreamReader sr = File.OpenText(filePath);
            while ((text = sr.ReadLine()) != null)
            {
                //Add it to the value string if it doesn't have the closing tag
                if (!text.Contains(svg))
                {
                    value += text + Environment.NewLine;
                }
            }
            //Close the reader
            sr.Close();
            //Write the string version without the closing tag
            File.WriteAllText(filePath, value);
        }
        public static string getString(string [] newar){
            int count=0;string pointsString="";
            for(int i=0;i<newar.Length;i++){
                if(count%2==0){
                    pointsString+=newar[i] + ", ";
                }else if(i==newar.Length-1){
                    pointsString+=newar[i];
                }else{
                    pointsString+=newar[i] + " ";
                }
                count++;
            }  
            return pointsString;
        }

    }

