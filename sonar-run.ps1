clear
[Environment]::SetEnvironmentVariable("JAVA_HOME", "C:\Program Files\Java\jdk-11.0.12\")
dotnet sonarscanner begin /o:"rlmariz" /k:"rlmariz_Teste-Texo" /d:sonar.host.url="https://sonarcloud.io" 
    dotnet build --configuration Release
dotnet sonarscanner end