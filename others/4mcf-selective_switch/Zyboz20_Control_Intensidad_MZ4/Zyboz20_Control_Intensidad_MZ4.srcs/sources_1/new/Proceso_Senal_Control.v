`timescale 1ns / 1ps
module Proceso_Senal_Control(
input sysclk,sync,EnControl,
input [4:0]ordenControl,
input [2:0]En_PM,
input [11:0]I_Actual,ref,pasos,
output reg CtrlOk=0,
output [36:0] dataDaC_C
    );
 
//    Proceso_Senal_Control PSC01(.sysclk(sysclk),.sync(sync),.Modoff(Modoff),.ordenControl(ordenControl),.pasos(pasos),.I_Actual(I_Actual),.ref(ref),.CtrlOk(CtrlOk),.dataDaC_C(dataDaC_C) );
 wire[11:0]signalT1;   
Mod_SenalTriangular s1(.sysclk(sysclk),.signalT(signalT1) );
//Mod_SenalTriangular s2(.sysclk(sysclk),.sync(sync),.signalT(signalT2) );
//Mod_SenalTriangular s3(.sysclk(sysclk),.sync(sync),.signalT(signalT3) );    
reg[11:0]dataDac_C1=0;
reg[11:0]dataDac_C2=0;
reg[11:0]dataDac_C3=0;   
assign dataDaC_C={dataDac_C3,dataDac_C2,dataDac_C1} ;

wire [11:0]SControl_max1,SControl_max2,SControl_max3;
reg EnCtrolmax=0;
Alg_ctrl_Max Algmax01(.sysclk(sysclk),.sync(sync),.En(EnCtrolmax),.I_Actual(I_Actual),
.ref(ref),.pasos(pasos),.SControl1(SControl_max1),.SControl2(SControl_max2)
,.SControl3(SControl_max3),.CtrlOk(CtrlOkmax),.En_PM(En_PM));
    
wire [11:0]SControl_min1,SControl_min2,SControl_min3;
reg EnCtrolmin=0;
Alg_ctrl_Min Algmin01(.sysclk(sysclk),.sync(sync),.En(EnCtrolmin),.I_Actual(I_Actual),
.ref(ref),.pasos(pasos),.SControl1(SControl_min1),.SControl2(SControl_min2)
,.SControl3(SControl_min3),.CtrlOk(CtrlOkmin),.En_PM(En_PM));


wire [11:0]SControl_ref1,SControl_ref2,SControl_ref3;
reg EnCtrolref=0;
Alg_ctrl_Ref Algref01(.sysclk(sysclk),.sync(sync),.En(EnCtrolref),.I_Actual(I_Actual),
.ref(ref),.deltaErr(12'd128),.pasos(pasos),.SControl1(SControl_ref1),.SControl2(SControl_ref2)
,.SControl3(SControl_ref3),.CtrlOk(CtrlOkref),.En_PM(En_PM));
  
//OneShot osN(.sysclk(sysclk), .signal(sync),.Os(oreadyDac)); 
always@(posedge sysclk)begin
    case(ordenControl)
    0:begin end
    ////Modulación
    5:begin 
        dataDac_C1<=0; 
        dataDac_C2<=0; 
        dataDac_C3<=0; 
    end
    10:begin dataDac_C1<=signalT1; end
    11:begin dataDac_C2<=signalT1; end
    12:begin dataDac_C3<=signalT1; end
    
    15:begin
     EnCtrolmax<=EnControl;
     EnCtrolmin<=0;
      EnCtrolref<=0;
    CtrlOk<=CtrlOkmax;
    dataDac_C1<=SControl_max1;
    dataDac_C2<=SControl_max2;
    dataDac_C3<=SControl_max3;
    end
    16:begin
    EnCtrolmax<=0; 
EnCtrolmin<=EnControl; 
 EnCtrolref<=0;
    CtrlOk<=CtrlOkmin;
    dataDac_C1<=SControl_min1;
    dataDac_C2<=SControl_min2;
    dataDac_C3<=SControl_min3;
    end
    17:begin
     EnCtrolmax<=0; 
EnCtrolmin<=0; 
 EnCtrolref<=EnControl;
    CtrlOk<=CtrlOkref;
    dataDac_C1<=SControl_ref1;
    dataDac_C2<=SControl_ref2;
    dataDac_C3<=SControl_ref3;
    end
    endcase	
end   
endmodule
