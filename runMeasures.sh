echo "KruskalMST,PrimeMST" >Performance.csv
for i in {1..10}; do
  dotnet run
done
