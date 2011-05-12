// 
//  multifasta-parser.cs
//  
//  Author:
//       Jacob Israel Cervantes Luevano <jacobnix@gmail.com>
//  
//  Copyright (c) 2008 Jacob 
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;

namespace CutFasta
{
	public class CutFasta
	{
		StreamReader sr;
		StreamWriter sw;
		string destDir;
		public CutFasta()
		{
			destDir = "";
		}
		
		public void Load(string sourceFile)
		{
			destDir = Path.GetDirectoryName(sourceFile); 
			sr = File.OpenText(Path.GetFullPath(sourceFile));
		}

		public void Cut()
		{
			string line = "";
			int index = 0;
			int sequenceFastaSegmentCounter = -1;
			char[] tokens = {'>'};
			string seqName = "";
			
			try {
			   do {

					line = sr.ReadLine();
					index = line.IndexOf(tokens[0]);
					if(index != -1)	{
						sequenceFastaSegmentCounter++;
						//seqName = line.Substring((index+1),(line.Length-index));
						seqName = line.Substring((index+1));
						Console.WriteLine("seqName: {0}",seqName);
						
						index = seqName.IndexOf(' ');
						
						if(index != -1) 
							seqName = seqName.Substring(0,index);

						if(sequenceFastaSegmentCounter > 0)
							sw.Close();

						//string newfastafile = destDir + "/" + sequenceFastaSegmentCounter + ".fasta"; 
						string newfastafile = destDir + "/" + seqName + ".fasta"; 
						Console.WriteLine("Creating new fasta file {0}",newfastafile);
						Console.WriteLine("Short seqName: {0}",seqName);
						sw = File.CreateText(newfastafile);
						sw.WriteLine(line);
					}
					else 
						sw.WriteLine(line);	
					
				}
			 	while(line!=null);
			}
			catch(Exception e) {
						;
			}
			sr.Close();
			sw.Close();
		 }
		
	}


	public class testCutFasta
	{
		public static void Main(string[] args)
		{
			CutFasta cf = new CutFasta();

			Console.WriteLine("Loading fasta file {0}", args[0]);
			
			cf.Load(args[0]);

			Console.WriteLine("Extracting from fasta file...");
			cf.Cut();

		}
	}
}
