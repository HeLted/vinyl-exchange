export default function urlPathSeparator(url){
    url =  url.replace("https://","")
    let pathIndex = url.indexOf("/");
    url =  url.substr(pathIndex);

    return url;

}