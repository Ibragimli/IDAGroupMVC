window.addEventListener('scroll' , function(e) {
    e.preventDefault();

    let $headers = document.getElementById('headers');

    if(window.pageYOffset > 50) {
        $headers.style.top = '0px';
        $headers.style.position = 'fixed';
    }else{
        $headers.style.top = '50px';
        // $headers.style.position = 'fixed';
    }

    // console.log(window.pageYOffset);
})