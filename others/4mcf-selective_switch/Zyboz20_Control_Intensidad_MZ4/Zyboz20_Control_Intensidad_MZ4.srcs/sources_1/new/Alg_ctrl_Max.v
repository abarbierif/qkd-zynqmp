`timescale 1ns / 1ps
module Alg_ctrl_Max(
input sysclk,sync,En,
input[11:0]I_Actual,ref,
input[11:0]pasos,
output [11:0]SControl1,SControl2,SControl3,
output reg CtrlOk=1,
input [2:0]En_PM
    );
reg EnControl1=0;
Ctrl_busquedaMax c1(.sysclk(sysclk),.sync(sync),.EnControl(EnControl1),.I_Actual(I_Actual),
.pasos(pasos),.SControl(SControl1),.CicloOk(CicloOk1));  

reg EnControl2=0;
Ctrl_busquedaMax c2(.sysclk(sysclk),.sync(sync),.EnControl(EnControl2),.I_Actual(I_Actual),
.pasos(pasos),.SControl(SControl2),.CicloOk(CicloOk2));  

reg EnControl3=0;
Ctrl_busquedaMax c3(.sysclk(sysclk),.sync(sync),.EnControl(EnControl3),.I_Actual(I_Actual),
.pasos(pasos),.SControl(SControl3),.CicloOk(CicloOk3));  

OneShot osN(.sysclk(sysclk), .signal(sync),.Os(osync)); 
reg[3:0]index=0;
always@(posedge sysclk)begin
if(~En) begin
index<=0;
end
else
    case(index)
    0:begin
         if(osync ) begin
          if(En_PM[0])begin index<=1;end
          else if(En_PM[1])begin index<=4;   end
          else if(En_PM[2])begin index<=7;  end
          else begin index<=0; CtrlOk<=0;   end
          end   
     end
    1:begin
        if(I_Actual>ref)begin
          CtrlOk<=1;  index<=0; 
        end
        else begin
          CtrlOk<=0;  index<=2; EnControl1<=1;
        end
     end
     2:begin
        if(~CicloOk1) begin  EnControl1<=0; index<=3; end
     end
     3:begin
         
        if(CicloOk1) begin 
         if(En_PM[1])begin index<=4;   end
         else if(En_PM[2])begin index<=7;  end
         else begin index<=0; end
        end // index<=4; 
     end
    4:begin
       if(I_Actual>ref)begin
          CtrlOk<=1;  index<=0; 
        end
        else begin
          CtrlOk<=0;  index<=5; EnControl2<=1;
        end
        
        
     end
     5:begin
     if(~CicloOk2) begin  EnControl2<=0; index<=6; end
     end
     6:begin
   
        if(CicloOk2) begin 
         if(En_PM[2])begin index<=7;   end
         else begin index<=0;  end
        end // index<=4; 
     end
      7:begin
        if(I_Actual>ref)begin
          CtrlOk<=1;  index<=0; 
        end
        else begin
          CtrlOk<=0;  index<=8; EnControl3<=1;
        end
     end
     8:begin
        if(~CicloOk3) begin  EnControl3<=0; index<=9; end
     end
     9:begin
         EnControl3<=0;
        if(CicloOk3) begin  index<=0;  end
         // index<=4; 
     end
     endcase
end 

    
endmodule