`timescale 1ns / 1ps
module SyncMaestro(
input sysclk,
input[31:0]Pmuestreo,
output Sync
    );
    //SyncMaestro sync01(.sysclk(sysclk),.Pmuestreo(Pmuestreo),.Sync(Sync)  );
    divClockS div01(.sysclk(sysclk),.clk1(Sync),.umbral(Pmuestreo));
endmodule
