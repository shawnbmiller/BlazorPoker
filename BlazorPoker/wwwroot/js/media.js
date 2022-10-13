var dingBuffer = null;
var context = null;
var request = null;

function loadDingSound(url) {
    request = new XMLHttpRequest();
    request.open('GET', url, true);
    request.responseType = 'arraybuffer';
    request.onload = () => {
        const audioData = request.response;
        context.decodeAudioData(audioData).then((decodedData) => {
            dingBuffer = decodedData
        });
    }
    request.send();
}
function initAudio() {
    context = new AudioContext();
    loadDingSound("sounds/ding.ogg");
}

function playSound() {
    context.resume().then(() => {
    var source = context.createBufferSource(); // creates a sound source
    source.buffer = dingBuffer;                    // tell the source which sound to play
    source.connect(context.destination);       // connect the source to the context's destination (the speakers)
    source.start(0);  // play the source now      
    });
                      
}
