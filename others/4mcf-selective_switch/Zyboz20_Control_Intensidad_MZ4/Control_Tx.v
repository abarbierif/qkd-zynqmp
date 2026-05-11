`timescale 1ns / 1ps
module Control_Tx(
input clk_100Mhz,Tick,EnMod,
output RsTx, 
////////////memoriaTx
input [32:0]dataADCtx,
input [48:0] dataDaCTx,
input[11:0]trigger,
input[31:0]ordenTx
);



////////////memoriaTx
wire[31:0]odataADC;
reg resetFifo=0,ReadFifo=0;//full
Fifo32x1024 f01(.sysclk(clk_100Mhz),.ReadFifo(ReadFifo),.reset(resetFifo),.dataADCtx({dataADCtx[32]&EnWT,dataADCtx[31:0]})
   ,.odataADC(odataADC),.full(fullFifo),.empty(empty));

////////////memoriaTx2
wire[31:0]odataADC2;
reg ReadFifo2=0;//full
Fifo32x1024 f02(.sysclk(clk_100Mhz),.ReadFifo(ReadFifo2),.reset(resetFifo),.dataADCtx({dataADCtx[32]&EnWT,dataDaCTx[31:0]})
   ,.odataADC(odataADC2),.full(fullFifo2),.empty(empty2));
   ////////////memoriaTx2
wire[31:0]odataADC3;
Fifo32x1024 f03(.sysclk(clk_100Mhz),.ReadFifo(ReadFifo2),.reset(resetFifo),.dataADCtx({dataADCtx[32]&EnWT,16'd0,dataDaCTx[47:32]})
   ,.odataADC(odataADC3),.full(fullFifo3),.empty(empty3));
wire[11:0]SignalTriguer=ordenTx[19:8];
ModTrigger trg01(.sysclk(clk_100Mhz), .resetFifo(resetFifo),.SignalTriguer(SignalTriguer),
.dataADCtx(dataADCtx),.dataDaCTx(dataDaCTx),.EnW(EnWT) );
//wire[47:0]odataDaCTx;
//reg resetFifoDAC=0,ReadFifoDAC=0;//full
//Fifo48x1024 f02(.sysclk(clk_100Mhz),.ReadFifo(ReadFifoDAC),.reset(resetFifoDAC),.EnW(dataADCtx[32]),.data(dataDaCTx[47:0])
//    ,.odata(odataDaCTx),.full(fullDAC),.empty(emptyDAC));


reg StartTx=0;
reg[7:0]BuferTx=125;
UART_Tx UAtrTx01 (
    .Tick(Tick), 
    .clk_100Mhz(clk_100Mhz), 
    .BuferTx(BuferTx), 
    .StartTx(StartTx), 
    .readyTx(readyTx), 
    .RsTx(RsTx)
    );

OneShot os0(.sysclk(clk_100Mhz), .signal(readyTx),.Os(oreadyTx)); 
//OneShot os1(.sysclk(clk_100Mhz), .signal(~EnMod),.Os(oEnMod)); 
 
//////////////////////////////////////////
reg[31:0]countBuferTx=0;	 
reg[7:0]ProcesoTx=0; 
wire[7:0]wProcesoTx=ordenTx[7:0];
//////////////////////////////////////////
reg[31:0]BuffSend32bit=0;
reg[47:0]BuffSend48bit=0;
reg[6:0]Proc32bit=0;
//////////////////////////////////////////
reg[8:0]SimADC=0;
reg[0:0] triggerOk=0;
reg[7:0]regTriggger=0,dtg=0;
always @(posedge clk_100Mhz)begin
case(ProcesoTx[7:0])
0:begin 
ProcesoTx[7:0]<=wProcesoTx;
end
////////proceso con trigger
10:begin
resetFifo<=1;
ProcesoTx[7:0]<=8'd0; 
end
11:begin
resetFifo<=0;
ProcesoTx[7:0]<=8'd0; 
end
////////proceso lectura de memoria
20:begin
if(fullFifo)begin 
StartTx<=1;  
ProcesoTx[7:0]<=8'd25;  
countBuferTx<=0;
end 
else ProcesoTx[7:0]<=8'd0;
end
25:begin
Proc32bit<=0;
    if(oreadyTx)begin 					//espera que la transmicion este completa
    countBuferTx<=countBuferTx+32'd1;  
    ///////////mascara de detección
    if(countBuferTx==0) 	BuferTx[7:0]<=8'd0;   	//envía el segundo bit,B[1]=0
    if(countBuferTx==1) 	BuferTx[7:0]<=8'd255; 	//envía el 3º bit,B[2]=255
    if(countBuferTx==2) 	BuferTx[7:0]<=8'd0;		//envía el 4º bit,B[3]=0
    if(countBuferTx==3) 	BuferTx[7:0]<=8'd254; 	//envía el 3º bit,B[2]=255
    if(countBuferTx==4) 	BuferTx[7:0]<=8'd20; 	//envía el 3º bit,B[2]=255
    if(countBuferTx==5)begin
        BuferTx[7:0]<=8'd255; 
        ProcesoTx[7:0]<=8'd26;
         countBuferTx<=0;
         SimADC<=0;
        end
    end	
end	

26:begin
        SimADC<=SimADC+1;
        if(&SimADC)  ProcesoTx[7:0]<=8'd29;
        else begin 
        ReadFifo<=1; 
        ProcesoTx[7:0]<=8'd27; 
        end
end
   
27:begin
  ReadFifo<=0;
  BuffSend32bit<=odataADC;
  ProcesoTx[7:0]<=8'd28;   
end			
28:begin
    if(oreadyTx)begin 					//espera que la transmicion este completa
    countBuferTx<=countBuferTx+32'd1;  
    ///////////mascara de detección
    if(countBuferTx==0) 	BuferTx[7:0]<=BuffSend32bit[7:0];   	//envía el 1er bit,B[1]=0
    if(countBuferTx==1) 	BuferTx[7:0]<=BuffSend32bit[15:8];   	//envía el 2º bit,B[2]=255
    if(countBuferTx==2) 	BuferTx[7:0]<=BuffSend32bit[23:16]; 		//envía el 3º bit,B[3]=0
    if(countBuferTx==3) 	BuferTx[7:0]<=BuffSend32bit[31:24];   	//envía el 4º bit,B[2]=255
        if(countBuferTx==4) begin
        BuferTx[7:0]<=255;
        ProcesoTx[7:0]<=8'd26;	
        countBuferTx<=0;						
        end
    end	
end	
29:begin
    if(oreadyTx)begin 
            countBuferTx <=32'd0;
            StartTx<=0;
            ProcesoTx[7:0]<=8'd0;
            resetFifo<=0;
    end
    else
    resetFifo<=1;
end

//reg resetFifo=0,ReadFifo=0;//full
30:begin
if(fullFifo)begin 
StartTx<=1;  
ProcesoTx[7:0]<=8'd31;  
countBuferTx<=0;
end 
else ProcesoTx[7:0]<=8'd0;
end
31:begin
Proc32bit<=0;
    if(oreadyTx)begin 					//espera que la transmicion este completa
    countBuferTx<=countBuferTx+32'd1;  
    ///////////mascara de detección
    if(countBuferTx==0) 	BuferTx[7:0]<=8'd0;   	//envía el segundo bit,B[1]=0
    if(countBuferTx==1) 	BuferTx[7:0]<=8'd255; 	//envía el 3º bit,B[2]=255
    if(countBuferTx==2) 	BuferTx[7:0]<=8'd0;		//envía el 4º bit,B[3]=0
    if(countBuferTx==3) 	BuferTx[7:0]<=8'd254; 	//envía el 3º bit,B[2]=255
    if(countBuferTx==4) 	BuferTx[7:0]<=8'd30; 	//envía el 3º bit,B[2]=255
    if(countBuferTx==5)begin
        BuferTx[7:0]<=8'd255; 
        ProcesoTx[7:0]<=8'd32;
         countBuferTx<=0;
         SimADC<=0;
        end
    end	
end	

32:begin
        SimADC<=SimADC+1;
        if(&SimADC)  ProcesoTx[7:0]<=8'd35 ;
        else begin 
        ReadFifo<=1; 
        ProcesoTx[7:0]<=8'd33; 
        end
end
   
33:begin
  ReadFifo<=0;
  BuffSend32bit<=odataADC;
  ProcesoTx[7:0]<=8'd34;   
end			
34:begin
    if(oreadyTx)begin 					//espera que la transmicion este completa
    countBuferTx<=countBuferTx+32'd1;  
    ///////////mascara de detección
    if(countBuferTx==0) 	BuferTx[7:0]<=BuffSend32bit[7:0];   	//envía el 1er bit,B[1]=0
    if(countBuferTx==1) 	BuferTx[7:0]<=BuffSend32bit[15:8];   	//envía el 2º bit,B[2]=255
    if(countBuferTx==2) 	BuferTx[7:0]<=BuffSend32bit[23:16]; 		//envía el 3º bit,B[3]=0
    if(countBuferTx==3) 	BuferTx[7:0]<=BuffSend32bit[31:24];   	//envía el 4º bit,B[2]=255
        if(countBuferTx==4) begin
        BuferTx[7:0]<=255;
        ProcesoTx[7:0]<=8'd32;	
        countBuferTx<=0;						
        end
    end	
end	

35:begin
    if(oreadyTx)begin 					//espera que la transmicion este completa
    countBuferTx<=countBuferTx+32'd1;  
    ///////////mascara de detección
    if(countBuferTx==0) 	BuferTx[7:0]<=8'd12;   	//envía el 1er bit,B[1]=0
    if(countBuferTx==1) 	BuferTx[7:0]<=8'd12;   	//envía el 2º bit,B[2]=255
    if(countBuferTx==2) 	BuferTx[7:0]<=8'd12; 		//envía el 3º bit,B[3]=0
    if(countBuferTx==3) 	BuferTx[7:0]<=8'd12;   	//envía el 4º bit,B[2]=255
        if(countBuferTx==4) begin
        BuferTx[7:0]<=255;
        ProcesoTx[7:0]<=8'd36;	
        countBuferTx<=0;						
        end
    end	
end	
//odataADC2;
//reg resetFifo2=0,ReadFifo2=0;//
36:begin
        SimADC<=SimADC+1;
        if(&SimADC)  ProcesoTx[7:0]<=8'd39 ;
        else begin 
        ReadFifo2<=1; 
        ProcesoTx[7:0]<=8'd37; 
        end
end
   
37:begin
ReadFifo2<=0; 
BuffSend48bit<={odataADC3[15:0],odataADC2[31:0]};
ProcesoTx[7:0]<=8'd38;   
end			
38:begin
    if(oreadyTx)begin 					//espera que la transmicion este completa
    countBuferTx<=countBuferTx+32'd1;  
    ///////////mascara de detección
    if(countBuferTx==0) 	BuferTx[7:0]<=BuffSend48bit[7:0];   	//envía el 1er bit,B[1]=0
    if(countBuferTx==1) 	BuferTx[7:0]<=BuffSend48bit[15:8];   	//envía el 2º bit,B[2]=255
    if(countBuferTx==2) 	BuferTx[7:0]<=BuffSend48bit[23:16]; 		//envía el 3º bit,B[3]=0
    if(countBuferTx==3) 	BuferTx[7:0]<=BuffSend48bit[31:24];   	//envía el 4º bit,B[2]=255
    if(countBuferTx==4) 	BuferTx[7:0]<=BuffSend48bit[39:32];   	//envía el 4º bit,B[2]=255
    if(countBuferTx==5) 	BuferTx[7:0]<=BuffSend48bit[47:40];   	//envía el 4º bit,B[2]=255
        if(countBuferTx==6) begin
        BuferTx[7:0]<=255;
        ProcesoTx[7:0]<=8'd36;	
        countBuferTx<=0;						
        end
    end	
end	

39:begin
    if(oreadyTx)begin 
            countBuferTx <=32'd0;
            StartTx<=0;
            if(trigger[11:8]==0)begin
            ProcesoTx[7:0]<=8'd0;
            resetFifo<=0;
            end
           else  ProcesoTx[7:0]<=8'd40;
    end
    else begin
    resetFifo<=1;
    end
end

40:begin
 case(trigger[11:8])
 0:begin regTriggger<=trigger[7:0]; end 
 1:begin regTriggger<=dataADCtx[7:0]; end 
 2:begin regTriggger<=dataADCtx[15:8];   end 
 3:begin regTriggger<= dataADCtx[23:16];   end 
 4:begin regTriggger<=dataADCtx[31:24];   end 
   
 endcase
 
    if(regTriggger>trigger[7:0])begin
            ProcesoTx[7:0]<=8'd0;
            resetFifo<=0;
    end
end



default begin ProcesoTx[7:0]<=8'd0; end 
endcase
end
endmodule
