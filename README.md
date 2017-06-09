# Examples of Azure HDInsight Application templates

### Using Ambari REST API to get list of nodes

Ambari API Request

```
curl -u $USERID:$PASSWD https://YOURCLUSTERNAME.azurehdinsight.net/api/v1/clusters/YOURCLUSTERNAME/hosts?fields=Hosts/rack_info,Hosts/host_name,Hosts/maintenance_state,Hosts/public_host_name,Hosts/cpu_count,Hosts/ph_cpu_count,Hosts/host_status,Hosts/last_heartbeat_time,Hosts/ip,Hosts/total_mem
```

Response

```
{
  "href" : "http://10.0.0.17:8080/api/v1/clusters/avhdi1/hosts?fields=Hosts/rack_info,Hosts/host_name,Hosts/maintenance_state,Hosts/public_host_name,Hosts/cpu_count,Hosts/ph_cpu_count,Hosts/host_status,Hosts/last_heartbeat_time,Hosts/ip,Hosts/total_mem",
  "items" : [
    {
      "href" : "http://10.0.0.17:8080/api/v1/clusters/avhdi1/hosts/ed10-avhdi1.5nucivycqmou3et4ol0fc3ibqa.cx.internal.cloudapp.net",
      "Hosts" : {
        "cluster_name" : "avhdi1",
        "cpu_count" : 4,
        "host_name" : "ed10-avhdi1.5nucivycqmou3et4ol0fc3ibqa.cx.internal.cloudapp.net",
        "host_status" : "HEALTHY",
        "ip" : "10.0.0.8",
        "last_heartbeat_time" : 1497039291429,
        "maintenance_state" : "OFF",
        "ph_cpu_count" : 4,
        "public_host_name" : "ed10-avhdi1.5nucivycqmou3et4ol0fc3ibqa.cx.internal.cloudapp.net",
        "rack_info" : "/default-rack",
        "total_mem" : 28810336
      }
    },
    {
      "href" : "http://10.0.0.17:8080/api/v1/clusters/avhdi1/hosts/ed20-avhdi1.5nucivycqmou3et4ol0fc3ibqa.cx.internal.cloudapp.net",
      "Hosts" : {
        "cluster_name" : "avhdi1",
        "cpu_count" : 4,
        "host_name" : "ed20-avhdi1.5nucivycqmou3et4ol0fc3ibqa.cx.internal.cloudapp.net",
        "host_status" : "HEALTHY",
        "ip" : "10.0.0.6",
        "last_heartbeat_time" : 1497039291769,
        "maintenance_state" : "OFF",
        "ph_cpu_count" : 4,
        "public_host_name" : "ed20-avhdi1.5nucivycqmou3et4ol0fc3ibqa.cx.internal.cloudapp.net",
        "rack_info" : "/default-rack",
        "total_mem" : 28810336
      }
    },
    {
      "href" : "http://10.0.0.17:8080/api/v1/clusters/avhdi1/hosts/ed22-avhdi1.5nucivycqmou3et4ol0fc3ibqa.cx.internal.cloudapp.net",
      "Hosts" : {
        "cluster_name" : "avhdi1",
        "cpu_count" : 4,
        "host_name" : "ed22-avhdi1.5nucivycqmou3et4ol0fc3ibqa.cx.internal.cloudapp.net",
        "host_status" : "HEALTHY",
        "ip" : "10.0.0.12",
        "last_heartbeat_time" : 1497039291723,
        "maintenance_state" : "OFF",
        "ph_cpu_count" : 4,
        "public_host_name" : "ed22-avhdi1.5nucivycqmou3et4ol0fc3ibqa.cx.internal.cloudapp.net",
        "rack_info" : "/default-rack",
        "total_mem" : 28810336
      }
    }
  ]
}
```