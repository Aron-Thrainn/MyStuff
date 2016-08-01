//argument0 = trigger type (enumerator)

for (var i=0; i<30; i+=1) {
    with statuseffect[i] {
        if (trigger[argument0] != noone) {
            with trigger[argument0] {
                scr_ste_trigger_activate();
            }
        }
    }
}

