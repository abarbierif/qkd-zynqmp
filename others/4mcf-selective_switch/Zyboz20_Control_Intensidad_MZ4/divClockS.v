`timescale 1ns / 1ps

module divClockS(
input sysclk,
output reg clk1=0,
input[31:0] umbral
    );
 //divClockS divS01(.sysclk(sysclk),.clk1(clk1),.umbral(umbral));
     
reg[31:0]count=0;
 always@(posedge    sysclk) begin
 
    if(count>=umbral) begin clk1<=1; count<=0;  end 
     else begin
     if(count==umbral/2)  clk1<=0;
      count<=count+1;
     end
 end
endmodule
