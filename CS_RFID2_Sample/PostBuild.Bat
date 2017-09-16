REM 
REM first parameter - Solution File name ( ex. MasterSolution ) 
REM second parameter- $(ProjectDir) ( where the project is compiled ex. KTone.Core\Utils ) 
REM third parameter - $(OutDir) ( ex. \bin\Debug ) 
REM forth parameter - $(TargetFileName)) Target Filename  ( ex. Utils.dll ) 
REM fifth parameter - $(solutionDir) Solutions Directory 

if %1[==[ goto error
if %2[==[ goto error
if %3[==[ goto error
if %4[==[ goto error
if %5[==[ goto error

if not %1== DeviceReader goto default

REM if %PROTECTOR%[ == [ goto wo_protector

REM START protector 
REM code to replace original dll by protected dll
REM if exist %2%3%4 echo " Generating protected file --- "
REM if exist %2%3%4 protector -neutral -cctor %4

REM if not exist %2%3protected\%4 echo "Protector failed..."
REM if not exist %2%3protected\%4 goto copyerror
REM copy %2%3protected\%4 %2%3
REM END protector 

REM :wo_protector

if not exist %5Build mkdir %5Build
if not exist %5Build\MC9000Device mkdir %5Build\MC9000Device
REM if not exist %5Build\MC9000Device\Doc mkdir %5Build\MC9000Device\Doc
REM if not exist %5Build\MC9000Device\Examples mkdir %5Build\MC9000Device\Examples
if not exist %5Build\MC9000Device\SDK mkdir %5Build\MC9000Device\SDK

if not exist %5Build\RD5000Device mkdir %5Build\RD5000Device
REM if not exist %5Build\RD5000Device\Doc mkdir %5Build\RD5000Device\Doc
REM if not exist %5Build\RD5000Device\Examples mkdir %5Build\RD5000Device\Examples
if not exist %5Build\RD5000Device\SDK mkdir %5Build\RD5000Device\SDK


if exist %2%3%4 copy  %2%3%4 %5Build\RD5000Device\SDK
if errorlevel 1 goto copyerror
if exist %2%3%4 echo " POSTBUILD: copying --- "  %2%3%4 %5Build\RD5000Device\SDK


if exist %2%3%4 copy  %2%3%4 %5Build\MC9000Device\SDK
if errorlevel 1 goto copyerror
if exist %2%3%4 echo " POSTBUILD: copying --- "  %2%3%4 %5Build\MC9000Device\SDK

REM for /f "tokens=1-2 delims=." %%a in ( 'echo %4' ) do (  
REM if exist %2%3%%a.pdb copy  %2%3%%a.pdb %5Build\DeviceReader\DeviceSDK
REM if errorlevel 1 goto copyerror 
REM if exist %2%3%%a.pdb echo " POSTBUILD: copying --- "  %2%3%%a.pdb %5Build\DeviceReader\DeviceSDK )

goto default

:default
goto done 

:error
echo " POSTBUILD: Insufficient parameters, copy ignored " 
goto done 

:ignore
echo " POSTBUILD: regular solutions file , copy ignored " 
goto done 

:copyerror
echo Copy failed 
exit 1

:done

