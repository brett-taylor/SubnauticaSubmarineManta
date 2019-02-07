namespace Submarines.Engine
{
    /**
     * Engine States for a submarine/vehicle that would work similra to the cyclops.
     * Submarines/vehicles can use this system if they want less states as well.
     * This system will not work if you need more engine states.
     */
    public enum EngineState
    {
        OFF,
        SLOW,
        NORMAL,
        FAST,
        SPECIAL
    }
}
