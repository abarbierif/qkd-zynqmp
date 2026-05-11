`timescale 1ns / 1ps
module Mod_SenalTriangular(
input sysclk,
output [11:0]signalT
    );

//Mod_SenalTriangular mst01(.sysclk(sysclk),.sync(sync),.signalT(signalT) );
divClock div01(.sysclk(sysclk),.clk1(sync),.umbral(32'd363)); 
    OneShot osN(.sysclk(sysclk), .signal(sync),.Os(osync)); 
reg[011:0]count=0;
reg signo=1;
assign signalT=count;
always@(posedge sysclk)begin
    if(osync) begin
    
        if(signo)begin
        count<=count+1;
        if(count>12'd4090)signo=0;
        end
        else begin
         count<=count-1;
         if(count<12'd10)signo=1;
        end
    
    end
end 
endmodule
