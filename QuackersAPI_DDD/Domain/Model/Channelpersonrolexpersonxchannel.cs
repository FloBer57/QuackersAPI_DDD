using System;
using System.Collections.Generic;

namespace QuackersAPI_DDD.Domain.Model;

public partial class ChannelPersonRoleXPersonXChannel
{
    public int Person_Id { get; set; }
    public int Channel_Id { get; set; }
    public DateTime? ChannelPersonRoleXpersonXchannelAffectDate { get; set; }
    public int ChannelPersonRole_Id { get; set; }  // Ajout de l'ID du rôle

    public virtual Person Person { get; set; }
    public virtual Channel Channel { get; set; }
    public virtual ChannelPersonRole ChannelPersonRole { get; set; }  // Ajout de la relation
}
