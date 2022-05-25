
# Publish to nuget

- bump version in project -> package section
- make the package, right-click on project and 'Pack'
- cd to etsm\bin\Release
- dotnet nuget push etsm.x.y.z.nupkg --api-key abcdefabcdefabcdefabcdef --source https://api.nuget.org/v3/index.json