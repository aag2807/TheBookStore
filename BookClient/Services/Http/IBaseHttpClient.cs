using System.Reactive;

namespace BookClient.Services.Http;

/// <summary>
/// The interface implemented by all http client services
/// </summary>
public interface IBaseHttpClient
{
    /// <summary>
    /// Returns an observable of type T from a get request to the specified url
    /// </summary>
    /// <param name="url"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IObservable<T> GetAsync<T>(string url);
    
    /// <summary>
    /// Posts the content to the specified url and returns an observable of type T
    /// </summary>
    /// <param name="url"></param>
    /// <param name="content"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IObservable<T> PostAsync<T>(string url, T content);
    
    /// <summary>
    ///  Puts the content to the specified url and returns an observable of type T
    /// </summary>
    /// <param name="url"></param>
    /// <param name="content"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IObservable<T> PutAsync<T>(string url, T content);
    
    /// <summary>
    ///  Patches the content to the specified url and returns an observable of type T
    /// </summary>
    /// <param name="url"></param>
    /// <param name="content"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    IObservable<T> PatchAsync<T>(string url, HttpContent content);
    
    /// <summary>
    /// Deletes the specified url and returns an observable of type Unit
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    IObservable<Unit> DeleteAsync(string url);
}