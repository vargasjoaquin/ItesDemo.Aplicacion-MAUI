using ItesDemo.APP.Configuration;
using ItesDemo.APP.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace ItesDemo.APP.Services;

public class ApiClient
{
    static HttpClient httpClient;
    private Uri URL = new Uri("http://172.23.0.92:5003/api"); 


    public ApiClient()
    {
        httpClient = new HttpClient();        
    }

    public async Task<LoginResponseModel> ValidarLogin(string _usuario, string _password)
    {                                                                                         //aca probar el punto de interrupcion
        try
        {
            var loginParams = new StringContent(
                    JsonConvert.SerializeObject(
                        new
                        {
                            usuario = _usuario,
                            password = _password,
                            // password = Encriptar.GetSHA256(_password),

                        }),
                        Encoding.UTF8, "application/json"
                    );

            var result = await httpClient.PostAsync($"{URL}/usuario", loginParams).ConfigureAwait(false);

            var json = await result.Content.ReadAsStringAsync();

            LoginResponseModel responseLogin;

            if (result.StatusCode == System.Net.HttpStatusCode.OK)
            {                
                responseLogin = JsonConvert.DeserializeObject<LoginResponseModel>(json);

                AppConfiguration.Token = responseLogin.token;

                return responseLogin;
            }
            else
            {
                responseLogin = null;
            }
            return responseLogin;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

        return null;

    }

    /* public async Task<UsuarioModel> ObtenerDatosPersona(string _dni)
    {

        var result = await httpClient.GetAsync($"{URL}/usuarios/ObtenerDatosPersonales/{_dni}").ConfigureAwait(false);

        var json = await result.Content.ReadAsStringAsync();

        UsuarioModel usuarioModel;

        if (result.StatusCode == System.Net.HttpStatusCode.OK)
        {
            usuarioModel = JsonConvert.DeserializeObject<UsuarioModel>(json);

            return usuarioModel;
        }
        else
        {
            usuarioModel = null;
        }

        return usuarioModel;
    } */

    public async Task<IEnumerable<ProductoModel>> ObtenerProductos()
    {
        try
        {
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer"+Configuration.AppConfiguration.Token);
            var json = await httpClient.GetStringAsync($"{URL}/producto");

            var result = JsonConvert.DeserializeObject<IEnumerable<ProductoModel>>(json);

            return result;

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
            return null;
        }
    }

    /* public async Task<IEnumerable<AlumnosMateriaModel>> ObtenerAlumnosMateria(string carreramateriadiv)
    {
        try
        {
            var json = await httpClient.GetStringAsync($"{URL}/asistencia/ObtenerAlumnosMateria/{carreramateriadiv}");

            var result = JsonConvert.DeserializeObject<IEnumerable<AlumnosMateriaModel>>(json);

            return result;

        }
        catch (Exception ex)
        {
            return null;
        }


    }

    public async Task<bool> ConfirmarAsistenciaMateria(List<AlumnosMateriaModel> listaAsistencia)
    {
        var data = new StringContent(
                    JsonConvert.SerializeObject(listaAsistencia),
                        Encoding.UTF8, "application/json"
                    );

        var result = await httpClient.PostAsync($"{URL}/asistencia", data);

        return result.StatusCode == System.Net.HttpStatusCode.OK;

    } */
}
