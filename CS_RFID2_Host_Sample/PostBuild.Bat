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

if not %1== HostReader goto default

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
if not exist %5Build\RD5000HostC# mkdir %5Build\RD5000HostC#
REM if not exist %5Build\RD5000HostC#\Doc mkdir %5Build\RD5000HostC#\Doc
REM if not exist %5Build\RD5000HostC#\Examples mkdir %5Build\RD5000HostC#\Examples
if not exist %5Build\RD5000HostC#\SDK mkdir %5Build\RD5000HostC#\SDK

if not exist %5Build\XRSeriesHostC# mkdir %5Build\XRSeriesHostC#
REM if not exist %5Build\XRSeriesHostC#\Doc mkdir %5Build\XRSeriesHostC#\Doc
REM if not exist %5Build\XRSeriesHostC#\Examples mkdir %5Build\XRSeriesHostC#\Examples
if not exist %5Build\XRSeriesHostC#\SDK mkdir %5Build\XRSeriesHostC#\SDK

if exist %2%3%4 copy  %2%3%4 %5Build\RD5000HostC#\SDK
if errorlevel 1 goto copyerror
if exist %2%3%4 echo " POSTBUILD: copying --- "  %2%3%4 %5Build\RD5000HostC#\SDK

if exist %2%3%4 copy  %2%3%4 %5Build\XRSeriesHostC#\SDK
if errorlevel 1 goto copyerror
if exist %2%3%4 echo " POSTBUILD: copying --- "  %2%3%4 %5Build\XRSeriesHostC#\SDK

REM for /f "tokens=1-2 delims=." %%a in ( 'echo %4' ) do (  
REM if exist %2%3%%a.pdb copy  %2%3%%a.pdb %5Build\HostReader
REM if errorlevel 1 goto copyerror 
REM if exist %2%3%%a.pdb echo " POSTBUILD: copying --- "  %2%3%%a.pdb %5Build\HostReader )

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

