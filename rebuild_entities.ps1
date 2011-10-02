DbMetal.exe --conn="Server=sean-dev; Database=Capt; Uid=captuser; Pwd=capt;" --provider=MySql --dbml=Capt\Models\Capt.dbml --language=C# --namespace=Capt.Models --pluralize --case=leave

Get-Content Capt\Models\Capt.dbml | ForEach-Object { $_ -replace "SByte", "Boolean" } | ForEach-Object { $_ -replace "Table Name=`"Capt.", "Table Name=`"" } | Set-Content Capt\Models\Capt.dbml2
rm Capt\Models\Capt.dbml
mv Capt\Models\Capt.dbml2 Capt\Models\Capt.dbml

DbMetal.exe --provider=MySql --code=Capt\Models\Capt.cs --language=C# --namespace=Capt.Models --pluralize --case=leave Capt\Models\Capt.dbml