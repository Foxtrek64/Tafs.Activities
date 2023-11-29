//
//  ApiResultConverter.cs
//
//  Author:
//       Devin Duanne <dduanne@tafs.com>
//
//  Copyright (c) TAFS, LLC.
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Remora.Results;
using Tafs.Activities.TafsAPI.Models.Modern;

namespace Tafs.Activities.TafsApi.Activities.Converters
{
    /// <summary>
    /// Provides a converter for transforming a raw JSON response into an <see cref="ApiResult{TEntity}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to convert.</typeparam>
    public sealed class ApiResultConverter<TEntity> : JsonConverter<ApiResult<TEntity?>>
    {
        // {
        //   result : {
        //    success: true,
        //    error: "foo"
        //   },
        //   data: []
        // }

        /// <inheritdoc/>
        public override ApiResult<TEntity?>? ReadJson(JsonReader reader, Type objectType, ApiResult<TEntity?>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);

            bool success = (bool)(obj.GetValue("result.success") ?? false);

            Result result = success ? Result.FromSuccess() : (Result)new GenericError((string?)obj.GetValue("result.error") ?? string.Empty);

            JToken? dataToken = obj.GetValue("data");

            return dataToken is not null
                ? new ApiResult<TEntity?>(result, serializer.Deserialize<TEntity>(dataToken.CreateReader()))
                : new ApiResult<TEntity?>(result, default);
        }

        /// <inheritdoc/>
        public override void WriteJson(JsonWriter writer, ApiResult<TEntity?>? value, JsonSerializer serializer)
        {
            if (value is null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartObject();
            writer.WritePropertyName("result");
            writer.WriteStartObject();
            writer.WritePropertyName("success");
            writer.WriteValue(value.APIResponse.IsSuccess);
            writer.WritePropertyName("error");
            writer.WriteValue(value.APIResponse.Error?.Message ?? string.Empty);
            writer.WriteEndObject();
            writer.WritePropertyName("data");
            serializer.Serialize(writer, value.Data);
            writer.WriteEndObject();
        }
    }
}
