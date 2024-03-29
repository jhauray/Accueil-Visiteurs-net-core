/*
 * Accueil visiteurs
 *
 * Voici la définition de l'API d'échange en une application SPA d'accueil, et son backend.
 *
 * OpenAPI spec version: 1.0.0
 * Contact: jeremy.hauray@mythalesgroup.io
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace back.net_core.Models
{ 
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Visite : IEquatable<Visite>
    { 
        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Hd
        /// </summary>
        [DataMember(Name="hd")]
        public DateTime Hd { get; set; }

        /// <summary>
        /// Gets or Sets Visiteur
        /// </summary>
        [DataMember(Name="visiteur")]
        public string Visiteur { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Visite {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Hd: ").Append(Hd).Append("\n");
            sb.Append("  Visiteur: ").Append(Visiteur).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Visite)obj);
        }

        /// <summary>
        /// Returns true if Visite instances are equal
        /// </summary>
        /// <param name="other">Instance of Visite to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Visite other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Id == other.Id ||
                    Id != null &&
                    Id.Equals(other.Id)
                ) && 
                (
                    Hd == other.Hd ||
                    Hd != null &&
                    Hd.Equals(other.Hd)
                ) && 
                (
                    Visiteur == other.Visiteur ||
                    Visiteur != null &&
                    Visiteur.Equals(other.Visiteur)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    if (Hd != null)
                    hashCode = hashCode * 59 + Hd.GetHashCode();
                    if (Visiteur != null)
                    hashCode = hashCode * 59 + Visiteur.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Visite left, Visite right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Visite left, Visite right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
