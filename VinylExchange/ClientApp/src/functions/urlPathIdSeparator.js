export default function urlPathIdSeparator(path){
 const pathId = path.substr(path.lastIndexOf("/")+1);
 return pathId;
}