namespace PixelVision8.Assembly
{
    /// <summary>
    ///     The 6502 Chip is an implementation of 6502 assembly,
    ///     with all the logic it needs to work correctly in the PixelVisionEngine.
    ///     It's an easy way to use Assembly in PV8+ with minor caveats.
    /// </summary>
    public partial class GameChip
    {
        public byte[] RAM = new byte[65536]; // a bit of RAM, i guess. only about 64kb

        public bool lockSpecs = false;
        public byte A; // register A
        public byte X; // register X
        public byte Y; // register Y
        public byte SP;    // stack pointer

        public ushort PC; // program counter
        public int clockSpeedHz = 1789773; // ntsc nes-ish, but i will make this changeable through chips
        public int addressBusSize = 16;     // 16-bit address bus
        public void SetClockSpeed(int hz) {
            if (lockSpecs)
                return;
            
            clockSpeedHz = hz;
        }
        public void SetPC(ushort address) {
            if (lockSpecs)
                return;

            PC = address;
        }
        public byte Read(ushort address) {
            return RAM[address];
        }
        public void Write(ushort address, byte val) {
            RAM[address] = val;
        }
        public ushort ReadWord(ushort address) {
            byte lo;
            byte hi;
            lo = Read(address);
            hi = Read(address+1);
            ushort combi = (ushort)((hi << 8) | lo);
            return combi;
        }
        public void Reset()
        {
            A = X = Y = 0;
            SP = 0xFD;
            PC = ReadWord(0xFFFC);
        }

    }
}
