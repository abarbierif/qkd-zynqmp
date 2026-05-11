`timescale 1ns / 1ps
module OneShot(
input sysclk, signal,                                         
output  Os
    );
//OneShot osN(.sysclk(sysclk), .signal(),.Os(Os1));   
reg[1:0]data=0;
assign Os=data[0]&~data[1];
always@(posedge sysclk)begin
data[1:0]<={data[0],signal};
end

endmodule
