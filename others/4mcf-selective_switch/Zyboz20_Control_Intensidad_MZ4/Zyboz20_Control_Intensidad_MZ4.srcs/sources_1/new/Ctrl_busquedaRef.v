`timescale 1ns / 1ps
module Ctrl_busquedaRef(
input sysclk,sync,EnControl,
input[11:0]error,
input[11:0]pasos,
output reg[11:0]SControl=0,
output reg CicloOk=1
    );
//Control_busquedaSimple c1(.sysclk(sysclk),.sync(sync),.EnControl(EnControl),.error(error),.pasos(pasos),.SControl(SControl),.CicloOk(CicloOk));  
OneShot osN(.sysclk(sysclk), .signal(sync),.Os(osync)); 
reg[1:0]index=0;
reg[11:0]Ianterior=0;

always@(posedge sysclk)begin
if(SControl>4000)SControl<=1000;
else if(SControl<100)SControl<=3000;
else  if(osync ) begin 
        Ianterior<=error; 
        case(index)
            0:begin 
                if(EnControl)begin 
                CicloOk<=0;  
                index<=1;
                SControl<=SControl+pasos;
                end
            end
            1:begin 
                if(error<Ianterior) begin
                SControl<=SControl+pasos;
                end
                else begin
                SControl<=SControl-2*pasos;
                index<=2;
                end
            end
            2:begin 
                if(error<Ianterior) begin
                SControl<=SControl-pasos;
                end
                else begin
                SControl<=SControl+pasos;
                index<=0;
                CicloOk<=1;
                end
            end 
    //        3:begin  if(EnControl)begin index<=0; CicloOk<=0; end end
            
        endcase
    end
end 
endmodule
 