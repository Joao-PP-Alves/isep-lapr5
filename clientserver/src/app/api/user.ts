/** 
 * esta classe funciona como um request em ajax para permitir 
 * fazer pedidos GET e POST
 */

import { ErrorMessage } from "formik";

export async function user(endpoint: RequestInfo, {body, ...customConfig} = {}): Promise<any> {
    const headers = {'Content-Type': 'application/json'}

    const config = {
        method: body ? 'POST' : 'GET',
        ...customConfig,
        headers: {
            ...headers,
            ...customConfig.headers,
        },
    }

    if(body){
        config.body = JSON.stringify(body);
    }

    let data
    try{
        const response = await window.fetch(endpoint, config)
        data = await response.json()
        if(response.ok){
            return data
        }
        throw new Error(response.statusText)
    } catch (error) {
        return Promise.reject(ErrorMessage ? ErrorMessage : data)
    }
    }
    
    user.get = function (endpoint: any, customConfig= {}) {
        return user(endpoint, {...customConfig, method: 'GET'})
    }

    user.post = function(endpoint: any, body: any, customConfig={}) {
        return user(endpoint, {...customConfig, body})
    }

