mkdir .dist | Out-Null

7z a ".\.dist\${env:_RELEASE_NAME}-${env:_RELEASE_VERSION}_${env:_RELEASE_CONFIGURATION}.zip" ".\src\Depressurizer\bin\Release\net9.0-windows7.0\*"
