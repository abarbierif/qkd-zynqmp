#include <stdio.h>
#include <sys/mman.h>
#include <fcntl.h>
#include <stdlib.h>
#include <stdint.h>
#include <unistd.h>

// GPIO address
#define GPIO0_BASE  0x41200000
#define GPIO0_RANGE 0x10000

int main(int argc, char *argv[]){
    
    if(argc != 2){
        printf("Usage: %s <0|1>\n", argv[0]);
	return 1;
    }

    int val = atoi(argv[1]);
    if(val != 0 && val != 1){
        printf("Error: argv[1] must be 0 or 1\n");
	return 1;
    }
    
    // Open /dev/mem
    int fd = open("/dev/mem", O_RDWR | O_SYNC);
    if(fd < 0){
        perror("Failed to open /dev/mem");
	return 1;
    }

    // Map GPIO0
    volatile uint32_t *gpio0 = (volatile uint32_t *) mmap(NULL, GPIO0_RANGE, PROT_READ | PROT_WRITE, MAP_SHARED, fd, GPIO0_BASE);
    if(gpio0 == MAP_FAILED){
        perror("mmap gpio0 failed");
	return 1;
    }

    // Close /dev/mem
    close(fd);

    // Write to GPIO0
    // gpio0[0] as output setting gpio0[1] (3-state control register)
    gpio0[1] = 0x0;
    gpio0[0] = val ? 0xF : 0x0;

    return 0;

}
