int FMOD5_System_Create(void **system, unsigned int ver);
int FMOD5_System_Create2(void **system) {
	return FMOD5_System_Create(system, 0x00020311);
}
