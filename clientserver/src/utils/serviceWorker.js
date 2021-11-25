// This optional code is used to register a service worker.
// register() is not called by default.

//a service worker is a script  that a browser runs in the background
//separate from  a web page, opening the door to features that don't
//need a web page or a user interaction.
//1.Service Workers require HTTPS. But to enable local testing, this restriction
// doesn't apply to localhost. This is for security reasons as a Service Worker 
//acts like a man in the middle between the web application and the server.
//2.With Create React App Service Worker is only enabled in the production environment,
//for example when running npm run build.

const isLocalhost = Boolean(
  window.location.hostname === 'localhost' ||
    // [::1] is the IPv6 localhost address.
    window.location.hostname === '[::1]' ||
    // 127.0.0.0/8 are considered localhost for IPv4.
    window.location.hostname.match(
      /^127(?:\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)){3}$/
    )
);

export function register(config){
  if(process.env.NODE_ENV == 'production' && 'serviceWorker' in navigator){
    //The URL constructor is available in all browsers that support sw.
    const publicUrl = new URL(process.env.PUBLIC_URL, window.location.href);
    if(publicUrl.origin != window.location.origin) {
      //Our service worker won't work if PUBLIC_URL is on a differente origin from
      //what our page is served on. This might happen if a CDN is used to server assets;
      //see https://github.com/facebook/create-react-app/issues/2374
      return;
    }

    window.addEventListener('load', () => {
      const swUrl = `${process.env.PUBLIC_URL}/service-work.js`;

      if(isLocalhost) {
        //This is running on localhost. Let's check if a service worker still exists or not.
        checkValidServiceWorker(swUrl, config);

        //Add some additional logging to localhost, pointing developers to the
        //service worker/PWA documentation.
        navigator.serviceWorker.ready.then(() => {
          console.log(
            'This application is being served by a service worker.'
          );
        });
      } else {
        registerValidSW(swUrl, config);
      }
    });
  }
}

//makes the registration of the service worker
function registerValidSW(swUrl, config){
  navigator.serviceWorker.register(swUrl).then(registration => {
    registration.onupdatefound = () => {
      const installingWorker = registration.installing;
      if(installingWorker == null) {
        return;
      }

      installingWorker.onstatechange = () => {
        if(installingWorker.state == 'installed'){
          if(navigator.serviceWorker.controller) {
            //the content that was updated on the cache is fetched, 
            //but the other service worker will still server teh older content until the tabs are closed
            //only when they are open again the new content will be displayed
            console.log(
              'There´s new content available. Please close the tabs and open them again to see the changes.'
            );

            //callback
            if(config && config.onUpdate){
              config.onUpdate(registration);
            }

          } else {
            //the new changes are loaded
            console.log('The changes were loaded and saved. You can access them offline.');

            //callback
            if(config && config.onSuccess){
              config.onSuccess(registration);
            }
          }
        }
      };
    };
  })
  .catch(error => {
    console.error('An error occured during the service worker registration.', error);
  });
}

//checks if the server worker is valid
function checkValidServiceWorker(swUrl, config){
  //checks if the service worker exists
  fetch(swUrl).then(response => {
    //if the service worker exists, we receive a js file
    const contentType = response.headers.get('content-type');
    if(
      response.status == 404 || (contentType!=null && contentType.indexOf('javascript') == -1)
    ) {
      //in the case the system cannot find a service worker
      navigator.serviceWorker.ready.then(registration => {
        registration.unregister().then(() => {
          window.location.reload();
        });
      });
    } else {
      //the service worker was found
      registerValidSW(swUrl, config);
    }
  }).catch(() => {
    console.log( 'The internet is not available. The app will run in offline mode.');
  });
}

export function unregister(){
  if('serviceWorker' in navigator){
    navigator.serviceWorker.ready.then(registration => {
      registration.unregister();
    });
  }
}