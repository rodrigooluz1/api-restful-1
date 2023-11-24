using System;
namespace API.Domain.Entities
{
	public class AssertionConcern
	{
		public AssertionConcern()
		{
		}

		/// <summary>
		/// Valida tamanho maximo
		/// </summary>
		/// <param name="stringValue"></param>
		/// <param name="maximum"></param>
		/// <param name="message"></param>
		/// <exception cref="DomainException"></exception>
		public static void AssertArgumentLength(string stringValue, int maximum, string message)
		{
			int length = stringValue.Trim().Length;
			if(length > maximum)
			{
				throw new DomainException(message);
			}
		}

		/// <summary>
		/// valida tamanho minimo e maximo
		/// </summary>
		/// <param name="stringValue"></param>
		/// <param name="minimum"></param>
		/// <param name="maximum"></param>
		/// <param name="message"></param>
		/// <exception cref="DomainException"></exception>
        public static void AssertArgumentLength(string stringValue, int minimum, int maximum, string message)
        {
            int length = stringValue.Trim().Length;
            if (length < minimum || length > maximum)
            {
                throw new DomainException(message);
            }
        }

		/// <summary>
		/// Verifica se string está vazia
		/// </summary>
		/// <param name="stringValue"></param>
		/// <param name="message"></param>
		/// <exception cref="DomainException"></exception>
        public static void AssertArgumentNotEmpty(string stringValue, string message)
        {
            if (stringValue == null || stringValue.Trim().Length == 0)
            {
                throw new DomainException(message);
            }
        }

		/// <summary>
		/// verifica se objeto é nulo
		/// </summary>
		/// <param name="object1"></param>
		/// <param name="message"></param>
		/// <exception cref="DomainException"></exception>
        public static void AssertArgumentNotNull(object object1, string message)
        {
            if (object1 == null)
            {
                throw new DomainException(message);
            }
        }

    }
}

