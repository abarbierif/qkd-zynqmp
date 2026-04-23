import os
import sys
import mmap
import struct

# GPIO0 ADDRESS
GPIO0_BASE  = 0x41200000
GPIO0_RANGE = 0x10000

if len(sys.argv) != 2:
    sys.exit(f"Usage: python3 {sys.argv[0]} <0|1>")

val = int(sys.argv[1])
if val !=0 and val !=1:
    sys.exit("Error: argv[1] must be 0 or 1")

# Open /dev/mem
try:
    fd = os.open("/dev/mem", os.O_RDWR | os.O_SYNC)
except OSError as e:
    print(f"Failed to open /dev/mem: {e}")

# Map GPIO0
try:
    gpio0 = mmap.mmap(
        fileno=fd,
        length=GPIO0_RANGE,
        flags=mmap.MAP_SHARED,                 #default
        prot=mmap.PROT_READ | mmap.PROT_WRITE, #default
        offset=GPIO0_BASE,
    )
except OSError as e:
    print(f"mmap gpio0 failed: {e}")
    sys.exit(1)

# Close /dev/mem
os.close(fd)

# Set direction register - all outputs
gpio0.seek(4)
gpio0.write(struct.pack("<i", 0x0))

# Write GPIO0
gpio0.seek(0)
gpio0.write(struct.pack("<i", 0xF if val else 0x0))
