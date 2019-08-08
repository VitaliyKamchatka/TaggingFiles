using System; using System.Collections.Generic; using System.Linq; using System.Text; using System.Threading.Tasks;
using System.IO;

namespace ConsoleDirWalk
{
    public class StackBasedIteration
    {
        public static void TraverseTree(string root)
        {
            String left_tab="---   ---"; int left_tab_cnt=3;
            // Data structure to hold names of subfolders to be             // examined for files.
            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(root)) throw new ArgumentException();

            dirs.Push(root);
            // left_tab += "         ";  left_tab_cnt++;

            while (dirs.Count > 0)
            {
                //  left_tab += "========="; left_tab_cnt++;
                // left_tab_cnt--; left_tab = "pop---___";for(int tmp_cnt=0;tmp_cnt <left_tab_cnt;tmp_cnt++) left_tab += "  +";

                string currentDir = dirs.Pop();
                string[] subDirs;
               
               // left_tab += "pus...,,,"; left_tab_cnt++;

                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                    Console.WriteLine("\n    ");
                    Console.WriteLine("in DIR: "+currentDir); 

                    left_tab += "   "; left_tab_cnt++;
                    
                }
                
               
                // An UnauthorizedAccessException exception will be thrown if we do not have
                // discovery permission on a folder or file. It may or may not be acceptable 
                // to ignore the exception and continue enumerating the remaining files and 
                // folders. It is also possible (but unlikely) that a DirectoryNotFound exception 
                // will be raised. This will happen if currentDir has been deleted by
                // another application or thread after our call to Directory.Exists. The 
                // choice of which exceptions to catch depends entirely on the specific task 
                // you are intending to perform and also on how much you know with certainty 
                // about the systems on which this code will run.
                catch (UnauthorizedAccessException e) { Console.WriteLine(e.Message); continue;  }
                catch (System.IO.DirectoryNotFoundException e) {Console.WriteLine(e.Message);continue; }

                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }

                catch (UnauthorizedAccessException e)  {Console.WriteLine(e.Message);continue;}
                catch (System.IO.DirectoryNotFoundException e) {Console.WriteLine(e.Message);continue;  }
                // Perform the required action on each file here.// Modify this block to perform your required task.
                foreach (string file in files)
                {
                    try
                    {
                        // Perform whatever action is required in your scenario.
                        System.IO.FileInfo fi = new System.IO.FileInfo(file);
                        Console.WriteLine("|"+left_tab + "{0}: \t {1}, \t {2}", fi.Name, fi.Length, fi.CreationTime);

                        if ( fi.Name.EndsWith(".tags.txt") )                         
                        {
                            Console.WriteLine("**** FILE DISCRIPTION FOUND   *****");

                        }
                    }
                    catch (System.IO.FileNotFoundException e)
                    {
                        // If file was deleted by a separate application
                        //  or thread since the call to TraverseTree()  // then just continue.
                        Console.WriteLine(e.Message);    continue;
                    }
                    
                }

                left_tab_cnt--; left_tab = "   "; for (int tmp_cnt=0; tmp_cnt <= left_tab_cnt; tmp_cnt++) left_tab += "   ";
                // left_tab_cnt--; left_tab = "pop---___"; for(int tmp_cnt=0;tmp_cnt <left_tab_cnt;tmp_cnt++) left_tab += "  +";
                //left_tab += "pus...,,,"; left_tab_cnt++;
                // Push the subdirectories onto the stack for traversal. // This could also be done before handing the files.
              
                Console.WriteLine(" foreach (string str in subDirs)   \n");

                foreach (string str in subDirs)
                { 
                    // left_tab_cnt--;left_tab = "+++++++++";for(int tmp_cnt=0;tmp_cnt<left_tab_cnt;tmp_cnt++) left_tab += "   ";
                 
                    dirs.Push(str);
                    // left_tab += " .  .  . "; left_tab_cnt++;
                    Console.WriteLine("[DIR] " +str ); //   + "    in     " // + subDirs);
                }

               
                } // end of while
           
        }

       




        class Program
        {

          

            static void Main(string[] args)
            {      // Specify the starting folder on the command line, or in 
                   // Visual Studio in the Project > Properties > Debug pane.
                   //TraverseTree(args[0]);
                
              //  TraverseTree("D:\\Docs\\projects\\vs2019");
                 TraverseTree("D:\\Docs\\projects\\.vscode\\ConsoleDirWalk_2019_08_Csharp");
             //   string DiscriptFileNameTmp = "D:\\Docs\\projects\\vs2019\\ConsoleDirWalk\\Docs\\File1.txt.tags.txt";
                string DiscriptFileNameTmp = "D:\\Docs\\projects\\.vscode\\ConsoleDirWalk_2019_08_Csharp\\Docs\\File1.txt.tags.txt";
               
               
                int itmp = 0;

                itmp = AnalizeDiscrptFile(DiscriptFileNameTmp);

                Console.WriteLine("Press any key");
                Console.ReadKey();
            }
             public static int  AnalizeDiscrptFile(string DiscriptFileName)
                {
                       Console.WriteLine(DiscriptFileName);
                // Example #1   // Read the file as one string.
                string textStr = System.IO.File.ReadAllText(DiscriptFileName);

                // Display the file contents to the console. Variable text is a string.
                System.Console.WriteLine("Contents of WriteText.txt = {0}", textStr);

                string[] ArrayOfTags = new string[99];
                int currentStringIndex = -1;

                for (int cnt = 0; cnt < textStr.Length ; cnt++)
                {

                  //  System.Console.WriteLine("cnt = {0}", cnt);
                  //  System.Console.WriteLine("textStr = {0}", textStr[cnt] );

                    if (textStr[cnt] == '#')
                    {
                        ++currentStringIndex;

                      //  System.Console.WriteLine("ArrayOfTags[{0}] = {1}", currentStringIndex, ArrayOfTags[currentStringIndex]);

                        if ( ( textStr[++cnt] != ' ') && (textStr[cnt] != '#'))
                        {
                            ArrayOfTags[currentStringIndex] += textStr[cnt];
                         //   System.Console.WriteLine("ArrayOfTags[{0}] = {1}", currentStringIndex,  ArrayOfTags[currentStringIndex] );
                                                                                   
                        }
                       /* else
                        {
                            System.Console.WriteLine("TAG FOUND = {0}", ArrayOfTags[currentStringIndex]);
                          //  currentStringIndex++;
                        }
                        */
                    }
                    else
                    {
                        ArrayOfTags[currentStringIndex] += textStr[cnt];
                    }


                }
                // disptaying the list of tags
                for (int cnt2 = 0; cnt2 <= currentStringIndex; cnt2++)      
                {
                    System.Console.WriteLine("ArrayOfTags[{0}] = {1}", cnt2, ArrayOfTags[cnt2]);
                }
                    return 0;
                }

        }
    } 
}
