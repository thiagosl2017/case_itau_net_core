using CaseItau.ConsoleAP.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CaseItau.ConsoleAP
{
    public class FundosRepository : BaseRepository
    {
        #region Atributos
        public static FundosEntitie _fundosEntite;
        public static List<FundosEntitie> _lstFundosEntite;        
        public static string mensagem;
        #endregion

        #region Métodos
        public async Task<List<FundosEntitie>> ObterTodosFundos()
        {
            _fundosEntite = new FundosEntitie();
            _lstFundosEntite = new List<FundosEntitie>();
            _responseMessage = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {

                    client.BaseAddress = new Uri(urlApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _responseMessage = await client.GetAsync("Fundo/obter-todos-fundos");

                    if (_responseMessage.IsSuccessStatusCode)
                    {
                        var responseString = await _responseMessage.Content.ReadAsStringAsync();
                        var dataJson = responseString == "" ? null : JObject.Parse(responseString)["dados"];

                        if (dataJson != null)
                        {
                            _lstFundosEntite = dataJson.ToObject<List<FundosEntitie>>();
                        }
                    }
                }                
                return _lstFundosEntite;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<FundosEntitie>> ObterFundoPorCodigo(string codigo)
        {
            _fundosEntite = new FundosEntitie();
            _lstFundosEntite = new List<FundosEntitie>();
            _responseMessage = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    _responseMessage = await client.GetAsync($"Fundo/obter-fundo-por-codigo/{codigo}");

                    if (_responseMessage.IsSuccessStatusCode)
                    {
                        var responseString = await _responseMessage.Content.ReadAsStringAsync();
                        var dataJson = responseString == "" ? null : JObject.Parse(responseString)["dados"];

                        if (dataJson != null)
                        {
                            _lstFundosEntite = dataJson.ToObject<List<FundosEntitie>>();
                        }
                    }
                }
                return _lstFundosEntite;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> CriarFundo(FundosEntitie fundo)
        {            
            _responseMessage = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var criarFundo = JsonConvert.SerializeObject(fundo);
                    var dataString = await PostHttpDataStringAsync($"Fundo/criar-fundo/", criarFundo);
                    var dataJson = dataString == "" ? null : JObject.Parse(dataString)["message"];

                    if (dataJson != null)
                    {
                        mensagem = dataJson.ToString();
                    }
                }
                return mensagem;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> MovimentarFundo(string codigo, decimal? patrimonio)
        {            
            _responseMessage = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var dataString = await PutHttpDataStringAsync($"Fundo/movimentar-patrimonio/{codigo}/{patrimonio}", string.Empty);
                    var dataJson = dataString == "" ? null : JObject.Parse(dataString)["message"];

                    if (dataJson != null)
                    {
                        mensagem = dataJson.ToString();
                    }
                }
                return mensagem;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> ExcluirFundo(string codigo)
        {
            _fundosEntite = new FundosEntitie();
            _responseMessage = new HttpResponseMessage();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApi);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var dataString = await DeleteHttpDataStringAsync($"Fundo/excluir-fundo/{codigo}");
                    var dataJson = dataString == "" ? null : JObject.Parse(dataString)["message"];

                    if (dataJson != null)
                    {
                        mensagem = dataJson.ToString();
                    }
                }
                return mensagem;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<string> AlterarFundo(string codigo, FundosEntitie fundo)
        {

            _fundosEntite = new FundosEntitie();
            _responseMessage = new HttpResponseMessage();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(urlApi);                    
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var alterarFundo = JsonConvert.SerializeObject(fundo);
                    var dataString = await PutHttpDataStringAsync($"Fundo/alterar-fundo/{codigo}", alterarFundo);
                    var dataJson = dataString == "" ? null : JObject.Parse(dataString)["message"];

                    if (dataJson != null)
                    {
                        mensagem = dataJson.ToString();
                    }
                }
                return mensagem;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
