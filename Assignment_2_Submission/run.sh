#set -o nounset
mcs Code/*.cs
mv Code/AccuracyWB.exe Code/Program.exe
mono Code/Program.exe Data_Files/training00.data Data_Files/training01.data Data_Files/training02.data Data_Files/training03.data Data_Files/training04.data Data_Files/phishing.train Data_Files/phishing.test Data_Files/phishing.dev
#rm Code/Program.exe
exit 0
