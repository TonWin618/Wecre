import service from "@/utils/service";


async function getProject(username:string,projectName:string){
    await service.get(`/api/${username}/${projectName}`);
}